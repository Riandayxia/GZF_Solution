/*
底部
*/
Ext.define('Main.Bottom', {
    extend: 'Ext.toolbar.Toolbar',
    xtype: 'main_bottom',
    style: 'background:#3892D3;',
    defaults: {
        style: 'color:#fff;'
    },
    items: [
        "© Copyright 重庆溯汇软件开发有限公司 版权所有 渝ICP备13007872号",
        '->',
        "Admin" + ',欢迎回来',
        '-',
        //'版本：' + idata.myInfo.siteversion
        '版本：V1.0'
    ]
});
