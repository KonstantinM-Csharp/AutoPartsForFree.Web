// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

<script>
    $(document).ready(function () {
        $("#download-btn").click(function () {
            // Отправляем AJAX запрос на сервер
            $.ajax({
                url: "/PriceList/Download",
                type: "POST",
                beforeSend: function () {
                    $("#progress-messages").html("<div class='alert alert-info'>Выполняется загрузка...</div>");
                },
                success: function (response) {
                    // Обновляем содержимое div с сообщениями о прогрессе
                    $("#progress-messages").html(response);
                },
                error: function (xhr, status, error) {
                    // Обработка ошибок
                    $("#progress-messages").html("<div class='alert alert-danger'>Ошибка: " + error + "</div>");
                }
            });
        });
    });
</script>