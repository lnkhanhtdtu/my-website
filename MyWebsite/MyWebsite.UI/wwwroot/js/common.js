/**
 * Hiển thị thông báo
 * show toaster [Success, Error, Warning, Infomation]
 * @param {*} type
 * @param {*} text
 * @param {*} timeOut
 */
function showToaster(type, text, timeOut = 5000) {
    $.toast({
        heading: type,
        text: text,
        position: "top-right",
        icon: type === "Infomation" ? "info" : type.toLowerCase(),
        hideAfter: timeOut,
    });
}

/**
 * Khởi tạo model view
 * @param {*} elementName 
 */
function tinyMCEInit(elementName) {
    tinymce.init({
        selector: elementName,
        plugins: ['advlist', 'autolink', 'lists', 'link', 'image', 'preview', 'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen', 'media', 'table', 'code'],
        toolbar_mode: 'floating',
        toolbar: 'undo redo | fontfamily fontsize blocks | bold italic underline strikethrough | backcolor | alignleft aligncenter alignright alignjustify | bullist numlist | outdent indent | link unlink | customInsertImageButton | preview media fullscreen | removeformat code',
        image_advtab: true,
        file_picker_types: 'image',
        file_picker_callback: function (cb, value, meta) {
            var input = document.createElement('input');
            input.setAttribute('type', 'file');
            input.setAttribute('accept', 'image/*');

            input.onchange = function () {
                var file = this.files[0];
                var reader = new FileReader();
                reader.onload = function () {
                    var id = 'blobid' + (new Date()).getTime();
                    var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                    var base64 = reader.result.split(',')[1];
                    var blobInfo = blobCache.create(id, file, base64);
                    blobCache.add(blobInfo);
                    cb(blobInfo.blobUri(), { title: file.name });
                };
                reader.readAsDataURL(file);
            };
            input.click();
        },
        height: 500
    });
}

// Phương thức này sẽ chuyển đổi một object thành một model view
function mapObjectToControlView(modelView) {
    if (typeof modelView !== "object") return;

    for (const property in modelView) {
        if (!modelView.hasOwnProperty(property)) continue;

        const capitalText = property.charAt(0).toUpperCase() + property.slice(1);
        const value = modelView[property];
        const $element = $(`#${capitalText}`);

        if (!$element.length) continue;

        if ($element.is('img') || property.toLowerCase().includes('image')) {
            handleImageElement(value);
        } else if ($element.attr('type') === 'date') {
            handleDateElement($element, value);
        } else if (typeof tinymce !== 'undefined' && tinymce.get(capitalText)) {
            handleTinyMCEElement(capitalText, value);
        } else if ($element.attr('type') === 'checkbox') {
            $element.prop('checked', value);
        } else {
            console.log("property: ", property);
            console.log("value: ", value);
            $element.val(value);
        }
    }
}

// Xử lý hình ảnh
function handleImageElement(value) {
    const imgSrc = `${value ? `data:image/png;base64,${value}` : '/images/default-image.png'}`;
    var previewImage = document.getElementById('previewImage');

    if (previewImage) {
        previewImage.src = imgSrc;
    } else {
        console.log("Lỗi!!!");
    }
}

// Xử lý ngày tháng
function handleDateElement($element, value) {
    if (value) {
        const dateValue = value.split('T')[0];
        $element.val(dateValue);
    }
}

// Xử lý TinyMCE
function handleTinyMCEElement(elementId, value) {
    tinymce.get(elementId).setContent(value || '');
}

// Format số tiền thành chữa định dạng VNĐ
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(amount);
}

// Xử lý validation on form submit
(function () {
    'use strict';

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation');

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit',
                function (event) {
                    if (!form.checkValidity()) {
                        event.preventDefault();
                        event.stopPropagation();
                    }

                    form.classList.add('was-validated');
                },
                false);
        });
})();
