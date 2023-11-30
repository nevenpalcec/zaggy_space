window.clipboardCopy = {
    copyText: function (text) {
        navigator.clipboard.writeText(text).then(function () {
            console.log("Copied to clipboard: " + text);
        })
            .catch(function (error) {
                alert(error);
            });
    }
};