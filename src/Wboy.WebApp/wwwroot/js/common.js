$.fn.ComboBox = function (options) {
    var defaults = {
        allowClear: false,
        placeholder: null,
        url: "",
        param: [],
        id: "",
        text: "",
        minimumResultsForSearch: -1
    };
    options = $.extend(defaults, options);
    var $select = $(this);
    $select.empty();
    $select.select2({
        minimumResultsForSearch: options.minimumResultsForSearch,
        allowClear: options.allowClear,
        placeholder: options.placeholder,
        language: "zh-CN"
    });

    if (options.url != "") {
        $.ajax({
            url: options.url,
            data: options.param,
            type: "GET",
            dataType: "json",
            async: false,
            success: function (data) {
                var json = data;
                json.forEach(function (item) {
                    $select.append("<option value='" + item[options.id] + "'>&nbsp;" + item[options.text] + "</option>");
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                OperateMsg(errorThrown, -1);
            }
        });
        
    }
    $select.val("");
    return $select;
}
/*********************公用方法**********************/

formatDate = function (v, format) {
    if (!v) return "";
    var d = v;
    if (typeof v == 'string') {
        if (v.indexOf("/Date(") > -1)
            d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
        else
            d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
    }
    var o = {
        "M+": d.getMonth() + 1,  //month
        "d+": d.getDate(),       //day
        "h+": d.getHours(),      //hour
        "m+": d.getMinutes(),    //minute
        "s+": d.getSeconds(),    //second
        "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
        "S": d.getMilliseconds() //millisecond
    };
    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
};
//计算文件大小函数(保留两位小数),Size为字节大小
countFileSize = function (Size) {
    var m_strSize = "";
    var FactSize = 0;
    FactSize = Size;
    if (FactSize < 1024.00)
        m_strSize = ToDecimal(FactSize) + " 字节";
    else if (FactSize >= 1024.00 && FactSize < 1048576)
        m_strSize = ToDecimal(FactSize / 1024.00) + " KB";
    else if (FactSize >= 1048576 && FactSize < 1073741824)
        m_strSize = ToDecimal(FactSize / 1024.00 / 1024.00) + " MB";
    else if (FactSize >= 1073741824)
        m_strSize = ToDecimal(FactSize / 1024.00 / 1024.00 / 1024.00) + " GB";
    return m_strSize;
};
//保留两位小数    
//功能：将浮点数四舍五入，取小数点后2位   
ToDecimal = function (x) {
    var f = parseFloat(x);
    if (isNaN(f)) {
        return 0;
    }
    f = Math.round(x * 100) / 100;
    return f;
};
//检查选择行
checkedRow = function (id) {
    var isOK = true;
    if (id == undefined || id == "" || id == 'null' || id == 'undefined') {
        isOK = false;
        swal("提示", "请选择行数据!", "error");
    } else if (id.split(",").length > 1) {
        isOK = false;
        swal("提示", "一次只能选择一条记录!", "error");
    }
    return isOK;
};

Loading = function (bool, text) {
    var ajaxbg = top.$("#loading_background,#loading_manage");
    top.$("#loading_manage").css("left", (top.$('body').width() - top.$("#loading_manage").width()) / 2);
    top.$("#loading_manage").css("top", (top.$('body').height() - top.$("#loading_manage").height()) / 2);
    if (bool) {
        ajaxbg.show();
        if (!text) {
            top.$("#loading_manage").html('<span class="mif-spinner4 mif-ani-spin mif-2x"></span> ' + text);
        } else {
            top.$("#loading_manage").html('<span class="mif-spinner4 mif-ani-spin mif-2x"></span> 正在拼了命为您加载…');
        }
    } else {
        ajaxbg.hide();
    }
};

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
};

$.fn.jqGridLoadComplete = function () {
    var records = $(this).jqGrid('getGridParam', 'records');
    if (records < 1) {
        $(".ui-jqgrid-bdiv").append('<div class="jfgrid-nodata-img" id="jfgrid_nodata_img_gridtable" style="position: absolute;top: 50%;left: 50%;height: 180px;width: 380px;margin: -90px 0 0 -190px;z-index: 1;"><img src="/images/nodata.jpg"></div>');
    } else {
        $("#jfgrid_nodata_img_gridtable").remove();
    }
};

/**************String 扩展***************/

String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    //var reg = new RegExp("({[" + i + "]})", "g");//这个在索引大于9时会有问题，谢谢何以笙箫的指出
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
};
String.format = function (str) {
    var args = arguments, re = new RegExp("%([1-" + args.length + "])", "g");
    return String(str).replace(re, function ($1, $2) {
        return args[$2];
    });
};
request = function (keyValue) {
    var search = location.search.slice(1);
    var arr = search.split("&&");
    for (var i = 0; i < arr.length; i++) {
        var ar = arr[i].split("=");
        if (ar[0] == keyValue) {
            if (unescape(ar[1]) == 'undefined') {
                return "";
            } else {
                return unescape(ar[1]);
            }
        }
    }
    return "";
};
/***********表单**********/

$.fn.GetWebControls = function (keyValue) {
    var reVal = "";
    $(this).find('input,select,textarea,.ui-select').each(function (r) {
        var id = $(this).attr('id');
        var type = $(this).attr('type');
        if (id != null && id != undefined && id != "") {
            switch (type) {
                case "checkbox":
                    if ($("#" + id).is(":checked")) {
                        reVal += '"' + id + '"' + ':' + '"true",';
                    } else {
                        reVal += '"' + id + '"' + ':' + '"false",';
                    }
                    break;
                case "select":
                    var value = $("#" + id).val();
                    if ($("#" + id).attr("multiple") == undefined)
                        reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",';
                    else {
                        reVal += '"' + id + '"' + ':' + '[';
                        if (value != null || value != undefined) {
                            $.each(value, function (index, ele) {
                                reVal += (index + 1 == value.length ? '"' + ele + '"' : '"' + ele + '",');
                            });
                        }
                        reVal += '],';
                    }
                    break;
                case "selectTree":
                    var value = $("#" + id).attr('data-value');
                    reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",';
                    break;
                case "file":
                    var value = $("#" + id).next().find("input").val();
                    reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",';
                    break;
                default:
                    var value = $("#" + id).val();
                    reVal += '"' + id + '"' + ':' + '"' + $.trim(value) + '",';
                    break;
            }
        }

    });
    reVal = reVal.substr(0, reVal.length - 1);
    if (!keyValue) {
        reVal = reVal.replace(/&nbsp;/g, '');
    }
    reVal = reVal.replace(/\\/g, '\\\\');
    reVal = reVal.replace(/\n/g, '\\n');
    var postdata = jQuery.parseJSON('{' + reVal + '}');
    //阻止伪造请求
    //if ($('[name=__RequestVerificationToken]').length > 0) {
    //    postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    //}
    return postdata;
};
$.fn.SetWebControls = function (data) {
    var $id = $(this);
    for (var key in data) {
        var id = $id.find('#' + key);
        if (id.attr('id')) {
            var type = id.attr('type');
            if (id.hasClass("Wdate")) {
                type = "datepicker";
            }
            var value = data[key];
            switch (type) {
                case "checkbox":
                    if (value == true) {
                        id.attr("checked", 'checked');
                        id.parent().addClass("checked");
                    } else {
                        id.removeAttr("checked");
                    }
                    break;
                case "select":
                    if (id.attr("multiple") == undefined)
                        id.select2("val", value);
                    else
                        id.val(value).select2();
                    //id.ComboBoxSetValue(value);
                    break;
                case "selectTree":
                    id.ComboBoxTreeSetValue(value);
                    break;
                case "datepicker":
                    id.val(formatDate(value, 'yyyy-MM-dd hh:mm'));
                    break;
                case "file":
                    id.next().find("input").val(value);
                    break;
                default:
                    id.val(value);
                    break;
            }
        }
    }
};

/***********弹出层封装**********/

$.SaveForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        loading: "正在处理数据...",
        success: null,
        async: false
    };
    //Loading(true, options.loading);
    options = $.extend(defaults, options);
    if ($('[name=__RequestVerificationToken]').length > 0) {
        options.param["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
    }
    window.setTimeout(function () {
        $.ajax({
            url: options.url,
            data: options.param,
            type: options.type,
            async: options.async,
            dataType: options.dataType,
            success: function (data) {
                //Loading(false);
                if (data.type == "3") {
                    dialogAlert(data.message, -1);
                } else {
                    dialogMsg(data.message, 1);
                    options.success(data);
                    dialogClose();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //Loading(false);
                top.dialogMsg(errorThrown, -1);
            },
            beforeSend: function () {
                //Loading(true, options.loading);
            },
            complete: function () {
                //Loading(false);
            }
        });
    }, 500);
};
$.SetForm = function (options) {
    var defaults = {
        url: "",
        param: [],
        type: "get",
        dataType: "json",
        success: null,
        async: false
    };
    options = $.extend(defaults, options);
    $.ajax({
        url: options.url,
        data: options.param,
        type: options.type,
        dataType: options.dataType,
        async: options.async,
        success: function (data) {
            if (data != null && data.type == "3") {
                dialogAlert(data.message, -1);
            } else {
                options.success(data);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            dialogMsg(errorThrown, -1);
        }, beforeSend: function () {
        },
        complete: function () {
        }
    });
};
$.SetFormPost = function (options) {
    var defaults = {
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null,
        async: false
    };
    options = $.extend(defaults, options);
    $.ajax({
        url: options.url,
        data: options.param,
        type: options.type,
        dataType: options.dataType,
        async: options.async,
        success: function (data) {
            if (data != null && data.type == "3") {
                dialogAlert(data.message, -1);
            } else {
                options.success(data);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            dialogMsg(errorThrown, -1);
        }, beforeSend: function () {
        },
        complete: function () {
        }
    });
};

$.RemoveForm = function (options) {
    var defaults = {
        msg: "注：您确定要删除吗？该操作将无法恢复",
        loading: "正在删除数据...",
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null
    };
    options = $.extend(defaults, options);
    dialogConfirm(options.msg, function (r) {
        if (r) {
            Loading(true, options.loading);
            window.setTimeout(function () {
                var postdata = options.param;
                if ($('[name=__RequestVerificationToken]').length > 0) {
                    postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                }
                $.ajax({
                    url: options.url,
                    data: postdata,
                    type: options.type,
                    dataType: options.dataType,
                    success: function (data) {
                        Loading(false);
                        if (data.type == "3") {
                            dialogAlert(data.message, -1);
                        } else {
                            dialogMsg(data.message, 1);
                            options.success(data);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        Loading(false);
                        dialogMsg(XMLHttpRequest.responseText, -1);
                    },
                    beforeSend: function () {
                        Loading(true, options.loading);
                    },
                    complete: function () {
                        Loading(false);
                    }
                });
            }, 500);
        }
    });
};

$.ConfirmAjax = function (options) {
    var defaults = {
        msg: "提示信息",
        loading: "正在处理数据...",
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null
    };
    options = $.extend(defaults, options);
    //Loading(true, options.loading);
    dialogConfirm(options.msg, function (r) {
        if (r) {
            window.setTimeout(function () {
                var postdata = options.param;
                if ($('[name=__RequestVerificationToken]').length > 0) {
                    postdata["__RequestVerificationToken"] = $('[name=__RequestVerificationToken]').val();
                }
                $.ajax({
                    url: options.url,
                    data: postdata,
                    type: options.type,
                    dataType: options.dataType,
                    success: function (data) {
                        Loading(false);
                        if (data.type == "3") {
                            dialogAlert(data.message, -1);
                        } else {
                            dialogMsg(data.message, 1);
                            options.success(data);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //Loading(false);
                        dialogMsg(XMLHttpRequest.responseText, -1);
                    },
                    beforeSend: function () {
                        // Loading(true, options.loading);
                    },
                    complete: function () {
                        //Loading(false);
                    }
                });
            }, 200);
        }
    });
};

dialogConfirm = function (content, callBack) {
    top.layer.confirm(content, {
        icon: 7,
        title: "提示",
        move: false,
        btn: ['确认', '取消']
    }, function (index) {
        callBack(true);
        top.layer.close(index);
    }, function () {
        callBack(false);
    });
};

dialogAlert = function (content, type) {
    if (type == -1) {
        type = 2;
    }
    top.layer.alert(content, {
        icon: type,
        title: "提示",
        move: false
    });
};

dialogMsg = function (content, type) {
    if (type == -1) {
        type = 2;
    }
    top.layer.msg(content, { icon: type, time: 2000, shift: 5, move: false });
};

dialogOpen = function (options) {
    //Loading(true);
    var defaults = {
        id: null,
        title: '',
        shade: 0.3,
        width: '100px',
        height: '100px',
        url: '',
        btn: ['确认', '关闭'],
        callBack: null
    };
    options = $.extend(defaults, options);
    var _url = options.url;
    var _width = parent.innerWidth > parseInt(options.width.replace('px', '')) ? options.width : parent.innerWidth + 'px';
    var _height = parent.innerHeight > parseInt(options.height.replace('px', '')) ? options.height : parent.innerHeight + 'px';
    top.layer.open({
        id: options.id,
        title: options.title,
        type: 2,
        move: false,
        resize: false,
        shade: options.shade,
        area: [_width, _height],
        content: _url,
        btn: options.btn,
        yes: function () {
            options.callBack(top.frames[top.$("#" + options.id + ">iframe").attr("name")]);
        },
        cancel: function () {
            return true;
        }
    });
};
dialogClose = function () {
    try {
        var index = top.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
        top.layer.close(index);
    } catch (e) {
        alert(e);
    }
};
reload = function () {
    location.reload();
    return false;
};

$.currentIframe = function () {
    var id = top.$(".page-tabs-content .active").data("id");
    var frame = undefined;
    top.$("iframe").each(function () {
        if ($(this).data("id") == id) {
            frame = top.frames[$(this).attr("name")];
        }
    });
    return frame;
};