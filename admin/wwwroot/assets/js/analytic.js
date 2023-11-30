
   // $('#menu_analytics_by_months').addClass('active');

    var analytics_by_month = {};

    analytics_by_month.load = async function () {

        let self = analytics_by_month;

        await self.ui.display_stats();
        await self.ui.display_stats_last_year();

    }

    analytics_by_month.ui = new function () {

        let self = analytics_by_month;

        this.display_stats = async function () {
            stat_year = 2022;

            //if (stat_year.validity.valid === false
            //    || stat_year.checkValidity() === false
            //    || !stat_year.value) {
            //    return;
            //}

            //export_to_pdf_button.style.visibility = 'hidden';
            //block($(tbody));

            let data = await self.bl.get_data();
            console.log(data);

            tbody.innerHTML = '';
            for (let [key, val] of Object.entries(data)) {

                let row = self.ui.make_row(val['pretty_name'], val);
                tbody.insertAdjacentHTML('beforeend', row);
            }

            export_to_pdf_button.style.visibility = '';
            remove_all_waitings();

        }

        this.display_stats_last_year = async function () {

            if (stat_year.validity.valid === false
                || stat_year.checkValidity() === false
                || !stat_year.value) {
                return;
            }

            export_to_pdf_button.style.visibility = 'hidden';
            block($(tbody));

            let data = await self.bl.get_data_last_year();
            console.log(data);

            tbody_last_year.innerHTML = '';
            for (let [key, val] of Object.entries(data)) {

                let row = self.ui.make_row(val['pretty_name'], val);
                tbody_last_year.insertAdjacentHTML('beforeend', row);
            }

            export_to_pdf_button.style.visibility = '';
            remove_all_waitings();

        }

        this.make_row = function (name, stat) {

            let value_key = stat['key'];

            let head_html = document.createElement('td');
            head_html.innerText = name;
            head_html = head_html.outerHTML;

            let months_html = stat['months'].map(month => {
                let m_td = document.createElement('td');
                m_td.innerText = month[value_key];
                return m_td.outerHTML;
            }).join('');

            let sum_html = document.createElement('strong');
            sum_html.innerText = stat['sum'];
            sum_html = '<td>' + sum_html.outerHTML + '</td>';

            let avg_html = document.createElement('strong');
            avg_html.innerText = stat['avg'];
            avg_html = '<td>' + avg_html.outerHTML + '</td>';

            let row_html = head_html + months_html + sum_html + avg_html;
            row_html = '<tr>' + row_html + '</tr>';
            return row_html;
        }
    }

    analytics_by_month.bl = new function () {

        let self = analytics_by_month;

        // Fetch and process data
        this.get_data = async function () {

            let year = stat_year.value;
            const response = await fetch('/api/rents/analytics_by_month/?year=' + year);
            self.data_raw = await response.json();

            self.data = self.bl.process_data(self.data_raw);
            return self.data;
        }


        // Fetch and process data
        this.get_data_last_year = async function () {

            let year = stat_year.value - 1;
            const response = await fetch('/api/rents/analytics_by_month/?year=' + year);
            self.data_raw = await response.json();

            self.data = self.bl.process_data(self.data_raw);
            return self.data;
        }

        // Round to 2 decimals if it is float number
        this.custom_round = function (num) {

            if (num % 1 === 0) {
                // Do not round integers
                return num;
            }
            else {
                // Round floats to 2 places
                return num.toFixed(2);
            }
        }

        this.make_pretty_name = function (stats) {

            for (let [name, stat] of Object.entries(stats)) {

                if (name == 'bookings') {
                    stat['pretty_name'] = 'Bookings';
                }
                else if (name == 'cancelations') {
                    stat['pretty_name'] = 'Cancellations';
                }
                else if (name == 'provision') {
                    stat['pretty_name'] = 'Provision';
                }
                else if (name == 'days_taken') {
                    stat['pretty_name'] = 'Days taken';
                }
                else if (name == 'average_daily_rate') {
                    stat['pretty_name'] = 'Average daily rate';
                }
                else if (name == 'book_cancellation_diff') {
                    stat['pretty_name'] = 'Booking difference';
                }
            }

        }

        this.process_data = function (stats) {

            // Map stat to its value
            let stat_k = {
                'provision': 'provision',
                'days_taken': 'days',
                'cancelations': 'rents',
                'bookings': 'rents',
                'average_daily_rate': 'price_daily',
                'book_cancellation_diff': 'rents_diff'
            };

            // Fill missing months
            for (let i = 1; i <= 12; ++i) {

                for (let [key, arr] of Object.entries(stats)) {

                    // If month not exist in stat
                    if (arr.findIndex(el => el['month'] == i) == -1) {

                        // Copy object with correct month
                        arr.push({
                            ...arr[0],
                            'month': i
                        });
                        // Set its value to 0
                        arr[arr.length - 1][stat_k[key]] = 0;
                    }
                }
            }

            // Make a new stat (booking rents - cancellation rents)
            stats['book_cancellation_diff'] = [];
            stats['bookings'].forEach((item, index) => {
                stats['book_cancellation_diff'].push({
                    month: item['month'],
                    rents_diff: item['rents'] - stats['cancelations'][index]['rents'],
                    what: 'rents_calc_diff',
                    year: item['year']
                });
            });

            // Calculate sum and average
            for (let [key, arr] of Object.entries(stats)) {

                // Sort by month
                arr.sort((a, b) => a['month'] - b['month']);

                // Make new obj
                let sum = arr.reduce((prev, cur) => prev + cur[stat_k[key]], 0);
                let avg = (sum / stats[key].length);
                stats[key] = {

                    'months': arr, // data by month
                    'key': stat_k[key], // key which holds month stat
                    'sum': self.bl.custom_round(sum), // sum of month values
                    'avg': self.bl.custom_round(avg) // Average of month values
                };
            }

            self.bl.make_pretty_name(stats);

            // Return processed stats
            return stats;
        }

    }

    //window.onload = analytics_by_month.load;
