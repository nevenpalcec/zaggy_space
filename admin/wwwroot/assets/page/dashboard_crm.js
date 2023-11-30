var dashboard_crm = {};

dashboard_crm.chart_inquries_by_day = async (data, chart_id) => {

    var json = JSON.parse(data);

    var days = [];
    var data = [];

    for (var d of json) {
        days.push(d['day_str']);
        data.push(d['inquries']);
    }


    var myChart = echarts.init(document.getElementById(chart_id));

    option = {
        xAxis: {
            data: days,
            type: 'category',
        },
        yAxis: {
            type: 'value',
        },
        series: [
            {
                data: data,
                type: 'line',
                smooth: false,
                areaStyle: {
                    color: {
                        type: 'linear',
                        x: 0,
                        y: 0,
                        x2: 0,
                        y2: 1,
                        colorStops: [{
                            offset: 0,
                            color: utils.rgbaColor(utils.getColors().primary, 0.5)
                        }, {
                            offset: 1,
                            color: utils.rgbaColor(utils.getColors().primary, 0)
                        }]
                    }
                },
                itemStyle: {
                    color: utils.getGrays().white,
                    borderColor: utils.getColor('primary'),
                    borderWidth: 2
                },
                lineStyle: {
                    color: utils.getColor('primary')
                },
                symbol: 'circle',
                grid: {
                    right: '3%',
                    left: '10%',
                    bottom: '10%',
                    top: '5%'
                }
            }
        ]
    };

    myChart.setOption(option);

}