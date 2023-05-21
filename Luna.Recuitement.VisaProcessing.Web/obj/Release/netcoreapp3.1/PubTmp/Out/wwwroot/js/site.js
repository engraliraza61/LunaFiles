// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('.select2').select2({
        closeOnSelect: false
    });
});
$(document).ready(function () {
    $("#AdministrationMenu").show();
    $("#HrMenu").show();
    $("#SelectionMenu").show();
    $("#ProcessingMenu").show();
    $("#DeploymentMenu").show();
    $("#FinanceMenu").show();
    $("#ReportsMenu").show();
    $("#ManagementMenu").hide();
    $('.applyDataTable').DataTable();
    //$('.applyDataTableWithExport').DataTable({
    //    dom: 'Bfrtip',
    //    buttons: [
    //        'csvHtml5',
    //        'pdfHtml5'
    //    ]
    //});
    $('.applyDataTableWithExport thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('.applyDataTableWithExport thead');
    $('#AdministrationMenu').attr("Enabled", false);
    $('#HrMenu').attr("Enabled", "False");
    var table = $('.applyDataTableWithExport').DataTable({

        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            //{
            //    extend: 'copyHtml5',                
            //    titleAttr: 'Copy',
            //    title: ''
            //},
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                titleAttr: 'Excel',
                title: ''
            },
            //{
            //    extend: 'csvHtml5',
            //    titleAttr: 'CSV',
            //    title: ''
            //},
            {
                extend: 'pdfHtml5', orientation: 'landscape',
                titleAttr: 'PDF',
                title: SYSTEMDATETIME(),
                pageSize: 'LEGAL',
                text: 'PDF',
                titleAttr: 'PDF',
                messageBottom: 'The information in this table is copyright to Sirius Cybernetics Corp.',
                
                //exportOptions: { columns: ':visible' },
                //customize: function (doc) {
                //    doc.defaultStyle.fontSize = 30;
                //}
            },
            //{ extend: 'print', text: 'Print' },
            //pdfMake.fonts = {
            //    Arial: {
            //        normal: 'arial.ttf',
            //        bold: 'arialbd.ttf',
            //        italics: 'ariali.ttf',
            //        bolditalics: 'arialbi.ttf'
            //    }
            //},
        ],
       
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    function SYSTEMDATETIME() {
        return Date;
    }
    var table = $('.applyDataTableWithExport1').DataTable({
        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            {
                extend: 'copyHtml5',
                titleAttr: 'Copy',
                title: ''
            },
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                title: ''
            },
            {
                extend: 'csvHtml5',
                titleAttr: 'CSV',
                title: ''
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    var table = $('.applyDataTableWithExport2').DataTable({
        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            {
                extend: 'copyHtml5',
                titleAttr: 'Copy',
                title: ''
            },
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                title: ''
            },
            {
                extend: 'csvHtml5',
                titleAttr: 'CSV',
                title: ''
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    var table = $('.applyDataTableWithExport3').DataTable({
        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            {
                extend: 'copyHtml5',
                titleAttr: 'Copy',
                title: ''
            },
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                title: ''
            },
            {
                extend: 'csvHtml5',
                titleAttr: 'CSV',
                title: ''
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    var table = $('.applyDataTableWithExport4').DataTable({
        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            {
                extend: 'copyHtml5',
                titleAttr: 'Copy',
                title: ''
            },
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                title: ''
            },
            {
                extend: 'csvHtml5',
                titleAttr: 'CSV',
                title: ''
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    var table = $('.applyDataTableWithExport5').DataTable({
        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            {
                extend: 'copyHtml5',
                titleAttr: 'Copy',
                title: ''
            },
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                title: ''
            },
            {
                extend: 'csvHtml5',
                titleAttr: 'CSV',
                title: ''
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    var table = $('.applyDataTableWithExport6').DataTable({
        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            //{
            //    extend: 'copyHtml5',
            //    titleAttr: 'Copy',
            //    title: ''
            //},
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                title: ''
            }
            //{
            //    extend: 'csvHtml5',
            //    titleAttr: 'CSV',
            //    title: ''
            //}
        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    var table = $('.applyDataTableWithExport7').DataTable({
        dom: 'Bfrtip',
        //buttons: [          
        //    {
        //        extend: 'collection',
        //        text: 'Export',
        //        buttons: [
        //            'copy',
        //            'excel',
        //            'csv'
        //        ]
        //    }            
        //],
        "buttons": [
            {
                extend: 'copyHtml5',
                titleAttr: 'Copy',
                title: ''
            },
            {
                extend: 'excelHtml5',
                titleAttr: 'Excel',
                title: ''
            },
            {
                extend: 'csvHtml5',
                titleAttr: 'CSV',
                title: ''
            }
        ],
        orderCellsTop: true,
        fixedHeader: true,
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" style="width:50px;" placeholder="' + title + '" />');

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
    $('.datatableHideFirstThree').DataTable({
        "columnDefs": [{
            "targets": [0, 1, 2], 
            "visible": false,
            "searchable": false
        }
        ]
    });
    $('.datatableHideFirstOnly').DataTable({
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }
        ]
    });
});
    function restrictInputOtherThanArabic($field)
        {
          // Arabic characters fall in the Unicode range 0600 - 06FF
          var arabicCharUnicodeRange = /[\u0600-\u06FF]/;

          $field.bind("keypress", function(event)
          {
            var key = event.which;
            // 0 = numpad
            // 8 = backspace
            // 32 = space
            if (key==8 || key==0 || key === 32)
            {
              return true;
            }

            var str = String.fromCharCode(key);
            if ( arabicCharUnicodeRange.test(str) )
            {
              return true;
            }

            return false;
          });
        }

        jQuery(document).ready(function() {
        // allow arabic characters only for following fields
        restrictInputOtherThanArabic($('.ArabicEntry'));
            restrictInputOtherThanArabic($('.ArabicEntry'));
        });
