function green() {

    let drag = false;
    let click = false;
    let start_id = ""; start_id_x = 0; start_id_y = 0;
    let end_id = "";
    let mousemove_id = ""; mousemove_id_x = 0; mousemove_id_y = 0;
    let sel_start_id = "1 _1"; sel_move_id = "1 _1";
    let last_x = 1; last_y = 1;

    

    table_1.addEventListener('mousedown', (e) => {
        drag = false;
        click = true;

        let tdUnderTheMouse = document.elementFromPoint(e.clientX, e.clientY);

        start_id = tdUnderTheMouse.id;
        start_id = start_id.replaceAll("_", "");

        start_id_x = start_id.split(" ")[0]
        start_id_y = start_id.split(" ")[1]
    }
    );

    table_1.addEventListener('mousemove', (e) => {

        if (click == true) {

            drag = true
            e.preventDefault();

            let tdUnderTheMouse = document.elementFromPoint(e.clientX, e.clientY);
            if (tdUnderTheMouse != null) {

                mousemove_id = tdUnderTheMouse.id.replaceAll("_", "");
                mousemove_id_x = mousemove_id.split(" ")[0];
                mousemove_id_y = mousemove_id.split(" ")[1];

                sel_start_id = start_id_x + ' _' + start_id_y

                for (var x = start_id_x; x <= last_x; x++) {
                    // refresh
                    sel_move_id = x + ' _' + last_y
                    document.getElementById(sel_move_id).style.background = ''
                }

                for (var x = start_id_x; x <= parseInt(mousemove_id_x); x++) {
                    // select row
                    last_x = parseInt(mousemove_id_x); last_y = mousemove_id_y
                    sel_move_id = x + ' _' + mousemove_id_y
                    document.getElementById(sel_move_id).style.background = '#ccc'

                }
                //console.log(start_id_x + ', ' + start_id_y + ' -> ' + mousemove_id_x + ', ' + mousemove_id_y)
            }
        }
    });

    table_1.addEventListener('mouseup', (e) => {

        if (drag == true && click == true) {

            click = false;
            let tdUnderTheMouse = document.elementFromPoint(e.clientX, e.clientY);

            end_id = tdUnderTheMouse.id;
            end_id = end_id.replaceAll("_", "");
            end_id_x = end_id.split(" ")[0]
            //console.log(tdUnderTheMouse)
            //console.log(start_id_x + ' ' + mousemove_id_y + ' -> ' + end_id)

            //open modal
            if (end_id_x != '') {

                console.log(' start:' + start_id_x + ' end:' + end_id_x)
                var date_from = $('#date_from').val()
                $('#datum-modal').modal('show');
                $('#object_name').val($('#name_' + mousemove_id_y).text() + ' ' + $('#name_' + mousemove_id_y).data("object"));
                $('#datum_calendar_from').val(addDays(date_from, start_id_x - 1));
                $('#datum_calendar_to').val(addDays(date_from, end_id_x - 1));

                for (var x = start_id_x; x <= last_x; x++) {
                    // refresh
                    sel_move_id = x + ' _' + last_y
                    document.getElementById(sel_move_id).style.background = '#008000'
                }
            }

        }
        else {

        }

    });

}

function addDays(date, days) {

    var result = new Date(date);

    result.setDate(result.getDate() + days);

    var d = result.getDate();
    var m = result.getMonth() + 1; //Month from 0 to 11
    var y = result.getFullYear();
    return '' + y + '-' + (m <= 9 ? '0' + m : m) + '-' + (d <= 9 ? '0' + d : d);
}