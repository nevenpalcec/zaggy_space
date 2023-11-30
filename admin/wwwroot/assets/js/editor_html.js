
function onClickBlazor(x) {
    DotNet.invokeMethodAsync('admin','AddTextToTextHistory', x.toString());
}

var MicroCode = (function () {
    var html_out = "";
    return {
        renderTimeout: function () {
            setTimeout(MicroCode.render, 300)
        },

        render: function () {
            $('.code-input').focus();
            $('#iframeid').attr("srcdoc", html_out);
        },

        init: function (inputSel, outputSel, languageSel) {
            this.focusInput(inputSel);
            this.listenForInput(inputSel);
            this.listenForLanguage(languageSel, '.code-output', inputSel);
            this.renderOutput(outputSel, $(inputSel)[0].value);
            this.listenerForScroll(inputSel, outputSel);
        },

        listenForInput: function (inputSel) {
            var self = this;

            $(inputSel).on('input keydown focus', function (key) {
                var input = this,
                    selStartPos = input.selectionStart,
                    inputVal = input.value;

                if (key.keyCode === 9) {
                    input.value = inputVal.substring(0, selStartPos) + "    " + inputVal.substring(selStartPos, input.value.length);

                    input.selectionStart = selStartPos + 4;
                    input.selectionEnd = selStartPos + 4;
                    key.preventDefault();
                }
                //console.log(inputVal);
                html_out = inputVal
                //$('#iframeid').attr("srcdoc", inputVal);

                self.renderOutput('.code-output', this.value);
            });
            Prism.highlightAll();
        },

        listenForLanguage: function (languageSel, outputSel, inputSel) {
            var self = this;
            $(languageSel).on('change', function () {
                $('code', outputSel)
                    .removeClass()
                    .addClass('language-markup')
                    .removeAttr('data-language');

                $(outputSel)
                    .removeClass()
                    .addClass('code-output language-' + this.value);

                $(inputSel)
                    .val('');

                $('code', outputSel)
                    .html('');

                self.focusInput(inputSel);
            });
        },

        listenerForScroll: function (inputSel, outputSel) {
            $(inputSel).on('scroll', function () {
                //console.log(this.scrollTop);
                $(outputSel)[0].scrollTop = this.scrollTop;
            });
        },

        renderOutput: function (outputSel, value) {
            $('code', outputSel)
                .html(value.replace(/&/g, "&amp;").replace(/</g, "&lt;")
                    .replace(/>/g, "&gt;") + "\n");

            Prism.highlightAll();
        },

        focusInput: function (inputSel) {
            var input = $(inputSel);
            //console.log(input);
            input.focus();

            input[0].selectionStart = input[0].value.length;
            input[0].selectionEnd = input[0].value.length;
        }
    }
})();