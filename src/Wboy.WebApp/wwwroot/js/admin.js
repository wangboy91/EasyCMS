$.fn.ComboBoxTree = function (options) {
    //options参数：description,height,allowSearch,appendTo,click,url,param,method,icon
    var $select = $(this);
    if (!$select.attr('id')) {
        return false;
    }
    if ($select.find('.ui-select-text').length == 0) {
        var $select_html = "";
        $select_html += "<div class=\"ui-select-text\"  style='color:#999;'>" + options.description + "</div>";
        $select_html += "<div class=\"ui-select-option\">";
        $select_html += "<div class=\"ui-select-option-content\" style=\"max-height: " + options.height + "\"></div>";
        if (options.allowSearch) {
            $select_html += "<div class=\"ui-select-option-search\"><input type=\"text\" class=\"form-control\" placeholder=\"搜索关键字\" /><span class=\"input-query\" title=\"Search\"><i class=\"mif-search\" title=\"按回车查询\"></i></span></div>";
        }
        $select_html += "</div>";
        $select.append($select_html);
    }


    var $option_html = $($("<p>").append($select.find('.ui-select-option').clone()).html());
    $option_html.attr('id', $select.attr('id') + '-option');
    $select.find('.ui-select-option').remove();
    if (options.appendTo) {
        $(options.appendTo).prepend($option_html);
    } else {
        $('body').prepend($option_html);
    }
    var $option = $("#" + $select.attr('id') + "-option");
    var $option_content = $("#" + $select.attr('id') + "-option").find('.ui-select-option-content');
    loadtreeview(options.url);
    function loadtreeview(url) {
        $option_content.treeview({
            onnodeclick: function (item) {
                $select.attr("data-value", item.id).attr("data-text", item.text);
                if (item.id == "") {
                    $select.find('.ui-select-text').html(item.text).css('color', '#999');
                } else {
                    $select.find('.ui-select-text').html(item.text).css('color', '#676A6C');
                }
                $select.trigger("change");
                if (options.click) {
                    options.click(item);
                }
            },
            height: options.height,
            url: url,
            param: options.param,
            method: options.method,
            description: options.description
        });
    }
    if (options.allowSearch) {
        $option.find('.ui-select-option-search').find('input').attr('data-url', options.url);
        $option.find('.ui-select-option-search').find('input').bind("keypress", function (e) {
            if (event.keyCode == "13") {
                var value = $(this).val();
                var url = changeUrlParam($option.find('.ui-select-option-search').find('input').attr('data-url'), "keyword", escape(value));
                loadtreeview(url);
            }
        }).focus(function () {
            $(this).select();
        });
    }
    if (options.icon) {
        $option.find('i').remove();
        $option.find('img').remove();
    }
    $select.find('.ui-select-text').unbind('click');
    $select.find('.ui-select-text').bind("click", function (e) {
        if ($select.attr('readonly') == 'readonly' || $select.attr('disabled') == 'disabled') {
            return false;
        }
        $(this).parent().addClass('ui-select-focus');
        if ($option.is(":hidden")) {
            $select.find('.ui-select-option').hide();
            $('.ui-select-option').hide();
            var left = $select.offset().left;
            var top = $select.offset().top + 38;
            var width = $select.width();
            if (options.width) {
                width = options.width;
            }
            if (($option.height() + top) < $(window).height()) {
                $option.slideDown(150).css({ top: top, left: left, width: width });
            } else {
                var _top = (top - $option.height() - 32);
                $option.show().css({ top: _top, left: left, width: width });
                $option.attr('data-show', true);
            }
            $option.css('border-top', '1px solid #e5e6e7');
            if (options.appendTo) {
                $option.css("position", "inherit")
            }
            $option.find('.ui-select-option-search').find('input').select();
        } else {
            if ($option.attr('data-show')) {
                $option.hide();
            } else {
                $option.slideUp(150);
            }
        }
        e.stopPropagation();
    });
    $select.find('li div').click(function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (!$(tar).hasClass('bbit-tree-ec-icon')) {
            $option.slideUp(150);
            e.stopPropagation();
        }
    });
    $(document).click(function (e) {
        var e = e ? e : window.event;
        var tar = e.srcElement || e.target;
        if (!$(tar).hasClass('bbit-tree-ec-icon') && !$(tar).hasClass('form-control')) {
            if ($option.attr('data-show')) {
                $option.hide();
            } else {
                $option.slideUp(150);
            }
            $select.removeClass('ui-select-focus');
            e.stopPropagation();
        }
    });
    return $select;
}
$.fn.ComboBoxTreeSetValue = function (value) {
    if (value == "") {
        return;
    }
    var $select = $(this);
    var $option = $("#" + $select.attr('id') + "-option");
    $select.attr('data-value', value);
    var data_text = $option.find('ul').find('[data-value=' + value + ']').html();
    if (data_text) {
        $select.attr('data-text', data_text);
        $select.find('.ui-select-text').html(data_text).css('color', '#676A6C');
        $option.find('ul').find('[data-value=' + value + ']').parent().parent().addClass('bbit-tree-selected');
    }
    return $select;
}

$.fn.jqGridRowValue = function (code) {
    var $jgrid = $(this);
    var json = [];
    var selectedRowIds = $jgrid.jqGrid("getGridParam", "selarrrow");
    if (selectedRowIds != undefined && selectedRowIds != "") {
        var len = selectedRowIds.length;
        for (var i = 0; i < len; i++) {
            var rowData = $jgrid.jqGrid('getRowData', selectedRowIds[i]);
            json.push(rowData[code]);
        }
    } else {
        var rowData = $jgrid.jqGrid('getRowData', $jgrid.jqGrid('getGridParam', 'selrow'));
        json.push(rowData[code]);
    }
    return String(json);
}
