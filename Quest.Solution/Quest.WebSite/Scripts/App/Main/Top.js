/*
头部
*/
Ext.define('Main.Top', {
    extend: 'Ext.container.Container',
    xtype: 'main_top',
    cls: 'app-header',
    height: 50,
    border: false,
    defaults: {
        xtype: 'component'
    },
    layout: {
        type: 'hbox',
        align: 'middle'
    },
    items: [
        {
            html: '溯汇软件',
            cls: 'app-header-title',
            width:'20%',
        },
        {
            flex: 1
        },
        {
            text: '设置',
            iconCls: 'icon_settings',
            height: 30,
            border: false,
            xtype: 'button',
            style: 'margin-right:10px;'
        }
    ]
});
