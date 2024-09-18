$.validator.addMethod('FileSize', function (value, element, param) {
    return this.optional(element) || element.files[0].size <= param;
});