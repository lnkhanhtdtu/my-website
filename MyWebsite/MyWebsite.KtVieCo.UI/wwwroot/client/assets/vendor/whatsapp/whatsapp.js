           // < !--WhatsApp: https://www.delightchat.io/whatsapp-chat-button-widget -->
<script src="~/client/assets/vendor/whatsapp/embed.min.js"></script>
//@* <script async src='https://d2mpatx37cqexb.cloudfront.net/delightchat-whatsapp-widget/embeds/embed.min.js'></script> * @
            < script >
            var wa_btnSetting = @Html.Raw(waBtnSetting);
var wa_widgetSetting = @Html.Raw(waWidgetSetting);
window.onload = () => {
    _waEmbed(wa_btnSetting, wa_widgetSetting);
};
        </script >