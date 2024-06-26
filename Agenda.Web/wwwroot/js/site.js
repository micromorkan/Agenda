﻿$("form").submit(function (event) {
    event.preventDefault();
});

(function ($) {
    $.fn.serializeObject = function () {
        var o = {};
        var form = this;
        var objects = [];
        var arrPrincCheck = [];
        form.find("[name]").each(function () {
            var pos = this.name.indexOf(".");
            if (pos === -1) {
                //o[this.name] = this.value;

                if ($(this).is(':checkbox')) {
                    o[this.name] = $(this).is(':checked');
                    arrPrincCheck.push(this.name);
                } else {
                    if (arrPrincCheck.indexOf(this.name) == -1) {
                        o[this.name] = this.value;
                    }
                }

            } else {
                var strObj = this.name.substring(0, pos);

                if (objects.indexOf(strObj) > -1) {
                    return true;
                }

                o[strObj] = {};
                var arrCheck = [];

                form.find("[name^=" + strObj + "]").each(function () {
                    var property = this.name.replace(strObj + ".", "");
                    if (o[strObj][property] === undefined) {
                        if ($(this).is(':checkbox')) {
                            o[strObj][property] = $(this).is(':checked');
                            arrCheck.push(property);
                        } else {
                            if (arrCheck.indexOf(property) == -1) {
                                o[strObj][property] = this.value;
                            }
                        }
                    }
                });

                objects.push(strObj);
            }
        });
        return o;
    };
})(jQuery);

(function ($, viewport) {
    if (viewport.is('xs')) {
        // ...
    }
    if (viewport.is('>=sm')) {
        // ...
    }
    if (viewport.is('<md')) {
        // ...
    }
    $(document).ready(function () {
        $(document).ajaxComplete(function () {
            $('#loading').fadeOut();
        });

        if ($("body").hasClass("nav-md")) {
            if (viewport.is('lg')) {
                $("#menu").addClass("menu_fixed");
            } else if (viewport.is('md')) {
                $("#menu").addClass("menu_fixed");
            } else {
                $("#menu").removeClass("menu_fixed");
            }
        } else {
            $("#menu").removeClass("menu_fixed");
        }

        $("#menu_toggle").on("click", function () {
            if ($("body").hasClass("nav-md")) {
                if (viewport.is('lg')) {
                    $("#menu").addClass("menu_fixed");
                } else if (viewport.is('md')) {
                    $("#menu").addClass("menu_fixed");
                } else {
                    $("#menu").removeClass("menu_fixed");
                }
            } else {
                $("#menu").removeClass("menu_fixed");
            }
        });
    });
    $(window).resize(
        viewport.changed(function () {
            if ($("body").hasClass("nav-md")) {
                if (viewport.is('lg')) {
                    $("#menu").addClass("menu_fixed");
                } else if (viewport.is('md')) {
                    $("#menu").addClass("menu_fixed");
                } else {
                    $("#menu").removeClass("menu_fixed");
                }
            } else {
                $("#menu").removeClass("menu_fixed");
            }
        })
    );

})(jQuery, ResponsiveBootstrapToolkit);

$.ajaxSetup({
    timeout: 1000000
});

function FillSelect(url, filter, selectId, selectedItem = null, compareValue = true) {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: url,
        data: filter,
        success: function (data) {
            if (compareValue) {
                $.each(data, function (i, item) {
                    $(selectId).append($('<option>', {
                        value: item.value,
                        text: item.text,
                        selected: selectedItem === item.value ? true : false,
                    }));
                });
            } else {
                $.each(data, function (i, item) {
                    $(selectId).append($('<option>', {
                        value: item.value,
                        text: item.text,
                        selected: selectedItem === item.text ? true : false,
                    }));
                });
            }
        },
        failure: function (response) {
            $('#result').html(response);
        }
    });
}

//function StartDatatables() {
//    $("#search").length && $("#search").DataTable({
//        paging: true,
//        searching: true,
//        lengthChange: false,
//        dom: "Blfrtip",
//        buttons: [{
//            extend: "excel",
//            className: "btn btn-primary btn-sm"
//        }, {
//            extend: "pdfHtml5",
//            className: "btn btn-primary btn-sm"
//        }, {
//            extend: "print",
//            className: "btn btn-primary btn-sm"
//        }],
//        responsive: !0
//    })
//}

function Create(ignoreValidation = false, sendFiles = false, defaultSelectValue = "", callback = null) {
    if (sendFiles) {
        var formData = new FormData(jQuery('#create')[0]);
        var form = $("#create");

        form.find("[type=file]").each(function () {
            formData.append(this.id, this.files[0]);
        });

        if ($("form").valid() || ignoreValidation) {
            $('#loading').show();
            $.ajax({
                url: form[0].action,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    if (data.success) {
                        ClearAllInputs(defaultSelectValue);
                        swal('Sucesso!', data.message, 'success');     
                        if (callback != null) {
                            callback();
                        }
                    } else {
                        if (data.errors) {
                            ShowModalListWarning(data.errors);
                        } else {
                            swal('Aviso!', data.message, 'warning');
                        }
                    }
                },
                error: function (xhr, status, error) {
                    ShowModalError('Não foi possível acessar o servidor. Entre em contato com o Administrador. ' + status + '. ' + error);
                }
            });
        } else {
            swal('Aviso!', 'Preencha todos os campos corretamente!', 'warning');
        }
    } else {
        var form = $("#create");
        var model = form.serializeObject();

        if ($("form").valid() || ignoreValidation) {
            $('#loading').show();
            $.ajax({
                url: form[0].action,
                type: 'POST',
                dataType: 'json',
                //contentType: 'application/json; charset=utf-8',
                data: model,
                //data: JSON.stringify(model),
                success: function (data) {
                    if (data.success) {
                        ClearAllInputs(defaultSelectValue);
                        swal('Sucesso!', data.message, 'success');         
                        if (callback != null) {
                            callback();
                        }
                    } else {
                        if (data.errors) {
                            ShowModalListWarning(data.errors);
                        } else {
                            swal('Aviso!', data.message, 'warning');
                        }
                    }
                },
                error: function (xhr, status, error) {
                    ShowModalError('Não foi possível acessar o servidor. Entre em contato com o Administrador. ' + status + '. ' + error);
                }
            });
        } else {
            swal('Aviso!', 'Preencha todos os campos corretamente!', 'warning');
        }
    }
}

function Edit(urlSucesso, ignoreValidation = false, sendFiles = false) {
    if (sendFiles) {
        var formData = new FormData(jQuery('#edit')[0]);
        var form = $("#edit");

        form.find("[type=file]").each(function () {
            formData.append(this.id, this.files[0]);
        });

        if ($("form").valid() || ignoreValidation) {
            $('#loading').show();
            $.ajax({
                url: form[0].action,
                type: 'POST',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: formData,
                success: function (data) {
                    if (data.success) {
                        swal({ title: 'Sucesso!', text: data.message, type: 'success', allowOutsideClick: false, allowEscapeKey: false }).then(function () { window.location.pathname = urlSucesso; });
                    } else {
                        if (data.errors) {
                            ShowModalListWarning(data.errors);
                        } else {
                            swal('Aviso!', data.message, 'warning');
                        }
                    }
                },
                error: function (xhr, status, error) {
                    ShowModalError('Não foi possível acessar o servidor. Entre em contato com o Administrador. ' + status + '. ' + error);
                }
            });
        } else {
            swal('Aviso!', 'Preencha todos os campos corretamente!', 'warning');
        }
    } else {
        var form = $("#edit");
        var model = form.serializeObject();
        if ($("form").valid()) {
            $('#loading').show();
            $.ajax({
                url: form[0].action,
                type: 'POST',
                dataType: 'json',
                //contentType: 'application/json; charset=utf-8',
                //data: JSON.stringify(model),
                data: model,
                success: function (data) {
                    if (data.success) {
                        swal({ title: 'Sucesso!', text: data.message, type: 'success', allowOutsideClick: false, allowEscapeKey: false }).then(function () { window.location.pathname = urlSucesso; });
                    } else {
                        if (data.errors) {
                            ShowModalListWarning(data.errors);
                        } else {
                            swal('Aviso!', data.message, 'warning');
                        }
                    }
                },
                error: function (xhr, status, error) {
                    ShowModalError('Não foi possível acessar o servidor. Entre em contato com o Administrador. ' + status + '. ' + error);
                }
            });
        } else {
            swal('Aviso!', 'Preencha todos os campos corretamente!', 'warning');
        }
    }
}

function Search(editar, urlEditar, excluir, urlExlcuir, pagina) {
    var form = $("#search");
    var model = form.serializeObject();
    var qtdPagina = 20;

    if (pagina == null) {
        pagina = 1;
    }

    $('#loading').show();
    $.ajax({
        url: form[0].action + "?page=" + pagina + "&qtPage=" + qtdPagina,
        type: 'POST',
        dataType: 'json',
        //contentType: 'application/json; charset=utf-8',
        //data: JSON.stringify(model),
        data: model,
        success: function (data) {

            if (data.success) {
                if (!$.fn.DataTable.isDataTable($('table'))) {
                    $('table').DataTable({
                        order: [],
                        paging: false,
                        searching: true,
                        lengthChange: false,
                        dom: "Blfrtip",
                        buttons: [{
                            extend: "excel",
                            className: "btn btn-primary btn-sm",
                            exportOptions: {
                                columns: 'th:not(:last-child)'
                            }
                        }, {
                            extend: "pdfHtml5",
                            orientation: 'landscape',
                            pageSize: 'A4',
                            className: "btn btn-primary btn-sm",
                            exportOptions: {
                                columns: 'th:not(:last-child)'
                            },
                            customize: function (doc) {
                                doc.content[1].margin = [100, 0, 100, 0];
                                //doc.styles.tableBodyOdd.alignment = 'center';
                                //doc.styles.tableBodyEven.alignment = 'center'; 
                            }
                        }, {
                            extend: "print",
                            className: "btn btn-primary btn-sm",
                            exportOptions: {
                                columns: 'th:not(:last-child)'
                            }
                        }],
                        "responsive": true,
                        "bAutoWidth": false,
                        "info": false,
                        "drawCallback": function (settings) {
                            AplicarCSSDTT();
                        }
                    });
                }

                var dt = $('table').DataTable().clear().draw();

                $.each(data.object, function (i, item) {
                    var row = [];

                    $('.column').each(function () {
                        var coluna = $(this).data('column');
                        var prefix = $(this).data('prefix');
                        var sufix = $(this).data('sufix');

                        if (coluna) {

                            var cols = coluna.split(".");

                            if (cols.length == 1) {
                                if (typeof (item[coluna]) === "boolean") {
                                    row.push(item[coluna] ? 'Sim' : 'Não');
                                } else if (item[coluna] !== null && item[coluna] !== undefined && moment(item[coluna], moment.ISO_8601, true).isValid()) {
                                    row.push(moment(item[coluna]).format("DD/MM/YYYY HH:mm:ss"));
                                } else {
                                    row.push(prefix + item[coluna] + sufix);
                                }
                            } else if (cols.length > 1) {
                                var value = item[cols[0]];

                                for (var i = 1; i < cols.length; i++) {
                                    value = value[cols[i]];
                                }

                                if (typeof (value) === "boolean") {
                                    row.push(value ? 'Sim' : 'Não');
                                } else if (item[coluna] !== null && item[coluna] !== undefined && moment(item[coluna], moment.ISO_8601, true).isValid()) {
                                    row.push(moment(item[coluna]).format("DD/MM/YYYY HH:mm:ss"));
                                } else {
                                    row.push(prefix + value + sufix);
                                }
                            }
                        }
                    });

                    if (editar && excluir) {
                        row.push("<a title='Editar' class='btn btn-xs btn-warning' href='" + urlEditar + "\/" + item.id + "'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a> " +
                            "<a title='Excluir' class='btn btn-xs btn-danger' onclick='DeleteConfirm(" + item.id + ", \"" + urlExlcuir + "\", " + editar + ", \"" + urlEditar + "\", " + excluir + ", \"" + urlExlcuir + "\", " + pagina + ", " + qtdPagina + ");'><span class='glyphicon glyphicon-trash' aria-hidden='true'></span></a>")
                    } else if (editar && !excluir) {
                        row.push("<a title='Editar' class='btn btn-xs btn-warning' href='" + urlEditar + "\/" + item.id + "'><span class='glyphicon glyphicon-edit' aria-hidden='true'></span></a>")
                    } else if (!editar && excluir) {
                        row.push("<a title='Excluir' class='btn btn-xs btn-danger' onclick='DeleteConfirm(" + item.id + ", \"" + urlExlcuir + "\", " + editar + ", \"" + urlEditar + "\", " + excluir + ", \"" + urlExlcuir + "\", " + pagina + ", " + qtdPagina + ");'><span class='glyphicon glyphicon-trash' aria-hidden='true'></span></a>")
                    }

                    dt.row.add(row).draw(false);
                });

                AplicarCSSDTT();
                AplicarDTT();

                RenderPagination(data.page, data.total, qtdPagina);
            } else {
                AplicarCSSDTT();
                AplicarDTT();

                if (data.errors) {
                    ShowModalListWarning(data.errors);
                } else {
                    ShowModalError(data.message);
                }
            }

          
        },
        error: function (xhr, status, error) {
            ShowModalError('Não foi possível acessar o servidor. Entre em contato com o Administrador. ' + status + '. ' + error);
        }
    });
}

function AplicarCSSDTT() {
    $('.column').each(function (y, col) {
        if ($(col).data('css')) {
            $('table tr td:nth-child(' + (y + 1) + ')').addClass($(col).data('css'));
        }
    });
}

function AplicarDTT() {
    if (!$.fn.DataTable.isDataTable($('table'))) {
        $('table').DataTable({
            order: [],
            paging: false,
            searching: true,
            lengthChange: false,
            dom: "Blfrtip",
            buttons: [{
                extend: "excel",
                className: "btn btn-primary btn-sm",
                exportOptions: {
                    columns: 'th:not(:last-child)'
                }
            }, {
                extend: "pdfHtml5",
                orientation: 'landscape',
                pageSize: 'A4',
                className: "btn btn-primary btn-sm",
                exportOptions: {
                    columns: 'th:not(:last-child)'
                },
                customize: function (doc) {
                    doc.content[1].margin = [100, 0, 100, 0];
                    //doc.styles.tableBodyOdd.alignment = 'center';
                    //doc.styles.tableBodyEven.alignment = 'center'; 
                }
            }, {
                extend: "print",
                className: "btn btn-primary btn-sm",
                exportOptions: {
                    columns: 'th:not(:last-child)'
                }
            }],
            "responsive": true,
            "bAutoWidth": false,
            "info": false,
            "drawCallback": function (settings) {
                AplicarCSSDTT();
            }
        });

        $('table tr').addClass('smallfont');
        $('table tr td a').addClass('smallfont');
    }

    if ($(".dataTables_length")) {
        $(".dataTables_length").hide();
    }

    $("div[id^=DataTables_Table_] div").last().hide();
}

function DeleteConfirm(idExclusao, urlExclusao, editar, urlEditar, excluir, urlExlcuir, pagina, qtdPagina) {
    swal({
        title: 'Deseja continuar a exclusão?',
        text: "Após confirmação, não será possível reverter!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#286090',
        cancelButtonColor: '#d9534f',
        confirmButtonText: 'Sim, deletar!',
        cancelButtonText: 'Não, cancelar!',
        confirmButtonClass: 'btn btn-primary',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false
    }).then(function () {
        $('#loading').show();
        $.ajax({
            url: urlExclusao,
            type: 'POST',
            data: { 'id': idExclusao },
            success: function (data) {
                if (data.success) {
                    swal('Sucesso!', data.message, 'success');
                } else {
                    if (data.errors) {
                        ShowModalListWarning(data.errors);
                    } else {
                        ShowModalError(data.message);
                    }
                }

                Search(editar, urlEditar, excluir, urlExlcuir, pagina, qtdPagina);
            },
            error: function (xhr, status, error) {
                ShowModalError('Não foi possível acessar o servidor. Entre em contato com o Administrador. ' + status + '. ' + error);
            }
        });
    }, function (dismiss) {
        if (dismiss === 'cancel') {
            swal('Cancelado', 'A exclusão foi cancelada pelo usuário!', 'error');
        }
    });
}

function MaskMoney() {
    var maxLength = '0.000.000,00'.length;

    $(".money").maskMoney({
        allowNegative: false,
        thousands: '.',
        decimal: ',',
        affixesStay: false
    }).attr('maxlength', maxLength).trigger('mask.maskMoney');
}

function MaskCpf() {
    $(".cpf").keydown(function () {
        try {
            $(".cpf").unmask();
        } catch (e) { }

        var tamanho = $(".cpf").val().length;

        $(".cpf").mask("999.999.999-99");

        var elem = this;

        setTimeout(function () {
            elem.selectionStart = elem.selectionEnd = 10000;
        }, 0);

        var currentValue = $(this).val();

        $(this).val('');
        $(this).val(currentValue);
    });
}

function MaskCnpj() {
    $(".cnpj").keydown(function () {
        try {
            $(".cnpj").unmask();
        } catch (e) { }

        var tamanho = $(".cnpj").val().length;

        $(".cnpj").mask("99.999.999/9999-99");

        var elem = this;

        setTimeout(function () {
            elem.selectionStart = elem.selectionEnd = 10000;
        }, 0);

        var currentValue = $(this).val();

        $(this).val('');
        $(this).val(currentValue);
    });
}

function ShowModalError(mensagem) {
    swal('Erro!', mensagem, 'error');
}

function ShowModalListWarning(errors) {
    var message = "";
    for (var i = 0; i < errors.length; i++) {
        message += errors[i].errorMessage + " <br> ";
    }
    swal('Aviso!', message, 'warning');
}

function ClearAllInputs(defaultSelectValue) {
    $('select').select2().val(defaultSelectValue).trigger("change");
    $('input').val('');
    $('textarea').val('');
}

function InputMaskTax(inputName) {
    $(inputName).inputmask('Regex', { regex: "^[0-9]{1,3}(\\,\\d{1,2})?$" });
}
function InputMaskNumber(inputName, maxDigits) {
    $(inputName).inputmask('Regex', { regex: "^[0-9]{1,"+maxDigits+"}$" });
}

function ConvertMoneyFloat(valor) {

    if (valor === "") {
        valor = 0;
    } else {
        valor = valor.replace("R$", "");
        valor = valor.replaceAll(".", "");
        valor = valor.replace(",", ".");
        valor = valor.trim();
        valor = parseFloat(valor);
    }

    return valor;
}

function MaskCep() {
    $(".cep").mask("99999-999");
}
function MaskRG() {
    $(".rg").mask("99.999.999-99");
}
function MaskPhone() {
    $(".phone").mask("(99) 99999-9999");
}
function MaskDate() {
    $(".dateFormat").mask("99/99/9999");
}
