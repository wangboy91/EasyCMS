function initWeixinShare(sharedata) {
    //var sharedata = {
    //    imgUrl: "http://res.3xianghe.com/img/logo.png",
    //    link: "http://nshare.sumxiang.com",
    //    desc: "照片、视频云端储存，图文混排多彩记录方式。用Own记录生活点滴，永久留存美好回忆。",
    //    title: "为你推荐Own——记录亲密而真实的时光，这里只有最亲密的人"
    //};
    function randomString (len) {
        len = len || 6;
        var chars = 'ABCDEFGHJKMNPQRSTWXYZabcdefhijkmnprstwxyz2345678';    /****默认去掉了容易混淆的字符oOLl,9gq,Vv,Uu,I1****/
        var maxPos = chars.length;
        var randomStr = '';
        for (i = 0; i < len; i++) {
            randomStr += chars.charAt(Math.floor(Math.random() * maxPos));
        }
        return randomStr;
    };
    var randomStr = randomString(16);
    var timestamp = new Date().getTime();
    var pageUrl = window.location.href;
    if (pageUrl.indexOf('#') > -1) {
        pageUrl = pageUrl.substring(0, pageUrl.indexOf('#'));
    }
    var strJson = { "noncestr": randomStr, "timestamp": timestamp, "pageurl": pageUrl };
    var url = "/Wx/GetWXsignature";
    axios.post(url, strJson)
        .then(function (response) {
            wx.config({
                debug: false,
                appId: 'wx31b3b3d1918f58b2',
                timestamp: timestamp,
                nonceStr: randomStr,
                signature: response.data.signature,
                jsApiList: [
                    'checkJsApi',
                    'onMenuShareTimeline',
                    'onMenuShareAppMessage',
                    'onMenuShareQQ',
                    'onMenuShareWeibo'
                ]
            });
            wx.ready(function () {
                wx.checkJsApi({
                    jsApiList: [
                        'checkJsApi',
                        'onMenuShareTimeline',
                        'onMenuShareAppMessage',
                        'onMenuShareQQ',
                        'onMenuShareWeibo'
                    ], // 需要检测的JS接口列表，所有JS接口列表见附录2,
                    success: function (res) {
                        //alert(JSON.stringify(res));
                        // 以键值对的形式返回，可用的api值true，不可用为false
                        // 如：{"checkResult":{"chooseImage":true},"errMsg":"checkJsApi:ok"}
                    },
                    fail: function (res) {
                        //alert(JSON.stringify(res));
                    }
                });
                // 发送给好友
                wx.onMenuShareAppMessage({
                    title: sharedata.title, // 分享标题
                    desc: sharedata.desc, // 分享描述
                    link: sharedata.link, // 分享链接
                    imgUrl: sharedata.imgUrl, // 分享图标
                    success: function (res) {

                    },
                    cancel: function (res) {

                    }
                });

                // 分享到朋友圈
                wx.onMenuShareTimeline({
                    title: sharedata.title + sharedata.desc, // 分享标题
                    desc: sharedata.desc, // 分享描述
                    link: sharedata.link, // 分享链接
                    imgUrl: sharedata.imgUrl, // 分享图标
                    success: function (res) {

                    },
                    cancel: function (res) {

                    }
                });

                // 分享到QQ
                wx.onMenuShareQQ({
                    title: sharedata.title, // 分享标题
                    desc: sharedata.desc, // 分享描述
                    link: sharedata.link, // 分享链接
                    imgUrl: sharedata.imgUrl, // 分享图标
                    success: function (res) {

                    },
                    cancel: function (res) {

                    }
                });
                //分享到QQ空间
                wx.onMenuShareQZone({
                    title: sharedata.title, // 分享标题
                    desc: sharedata.desc, // 分享描述
                    link: sharedata.link, // 分享链接
                    imgUrl: sharedata.imgUrl, // 分享图标
                    success: function () {
                        // 用户确认分享后执行的回调函数

                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数

                    }
                });


            });
        })
        .catch(function (error) {
            console.log(error);
        });
}

