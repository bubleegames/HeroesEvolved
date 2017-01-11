S.SSCT.General=Backbone.Controller.extend({initialize:function(){},getSourcePageInfo:function(s){var e=s.success?s.success:function(){},r=s.error?s.error:function(){},n=S.sdkBase.request(S.sdkBase.servType.COMMON,S.sdkBase.actions.GET_SOURCE_PAGE_INFO);n.success&&n.data?e.apply(this,[n.data]):r.apply()},keepBusinessCache:function(s,e){if(_.isUndefined(s)||_.isEmpty(s)||_.isUndefined(s.business))return e.error&&e.error("parm exception!"),void 0;var r=S.sdkBase.request(S.sdkBase.servType.COMMON,S.sdkBase.actions.GET_PAGE_PARAMS,{key:"business_cache_flags"})||{};_.isUndefined(r.data)?e.error("businessCacheFlags exception!"):r=JSON.parse(r.data);var n=[];_.isArray(s.business)?_.each(s.business,function(s){n.push(s)}):n.push(s.business),_.each(rConfig.businessCache,function(s){r[s]=_.indexOf(n,s)>=0?1:0}),S.sdkBase.request(S.sdkBase.servType.COMMON,S.sdkBase.actions.SET_PAGE_PARAMS,{key:"business_cache_flags",value:JSON.stringify(r)})},jumpToNativePage:function(s){S.sdkBase.request(S.sdkBase.servType.COMMON,S.sdkBase.actions.JUMP_TO_SDKPAGE,s)},getCacheData:function(s,e){return ucf.Cache.get(s,e)},setCacheData:function(s,e,r,n){ucf.Cache.set(s,e,r,n)},smsSendMessage:function(s,e){var r=rConfig.SMSContent[s];if(r){var n=S.sdkBase.request(S.sdkBase.servType.PHONE,S.sdkBase.actions.SMS_SEND_MESSAGE,{dstAddr:r.dstAddr,text:r.text});console.log("result",n,e),n.success?e.success&&e.success():e.error&&e.error(n.msg)}else e.error&&e.error("移动运营商类型错误 ！")},getClientConfig:function(s){var e=s&&s.success?s.success:function(){},r=s&&s.error?s.error:function(){},n=S.sdkBase.request(S.sdkBase.servType.COMMON,S.sdkBase.actions.GET_CLIENT_CONFIG);n.success?e.apply(this,[n.data]):r.apply(this,[n])},sendClientMsg:function(s,e){var r=e&&e.success?e.success:function(){},n=e&&e.error?e.error:function(){},t=S.sdkBase.request(S.sdkBase.servType.COMMON,S.sdkBase.actions.TOAST,{msg:s});t.success?r.apply(this):n.apply(this)},sendClientLoading:function(s,e){var r=e&&e.success?e.success:function(){},n=e&&e.error?e.error:function(){};this.quietRequestSync(S.sdkBase.servType.COMMON,S.sdkBase.actions.LOADING_INDICATOR_SHOW,{msg:s,success:r,error:n})},hideClientLoading:function(s){var e=s&&s.success?s.success:function(){},r=s&&s.error?s.error:function(){};this.quietRequestSync(S.sdkBase.servType.COMMON,S.sdkBase.actions.HIDE_LOADING_INDICATOR,{success:e,error:r})},setHeaderStyle:function(s){S.sdkBase.request(S.sdkBase.servType.COMMON,S.sdkBase.actions.SET_UI_STYLE,s)},clearHistory:function(){S.sdkBase.request(S.sdkBase.servType.APP,S.sdkBase.actions.CLEAR_HISTORY)},quietRequestSync:function(s,e,r){S.sdkBase.request(s,e,r,null,null,!0,null,{quiet:!0})},quietRequestAsync:function(s,e,r){var n=r.success||function(){},t=r.error||function(){};r.quiet=!0,r.data=r.data||{},S.sdkBase.request(s,e,r.data,n,t,!0,30,r)},jumpToLoginWidget:function(s,e){this.quietRequestAsync(S.sdkBase.servType.LOGIN,S.sdkBase.actions.JUMP_TO_LOGIN_WIDGET,{data:s,success:e.success,error:e.error})},loadUrl:function(s,e,r,n){if(!_.isEmpty(s)){wait=n||0,e=e||!0,r=r||!1;var t={url:s,props:{wait:wait,openexternal:e,clearhistory:r}};S.sdkBase.request(S.sdkBase.servType.APP,S.sdkBase.actions.LOAD_URL,t)}}});