Ext.define('QST.Personal.Top', {
    extend: 'Ext.Container',
    xtype: 'personal_top',
    alternateClassName: 'personal',
    config: {
        layout: 'vbox',
        height: 240,
        cls: 'personal',
        defaults: {
            flex: 1,
            layout: {
                type: 'hbox',
                align: 'top'
            },
            defaults: {
                flex: 1,
                xtype: 'button',
                iconAlign: 'top'
            }
        },
        items: [
        {
            items: [
                {
                    xtype: 'panel',
                }, {
                    flex: 6,
                    cls: 'myInfo',
                    iconCls: 'user_img',
                    text: '我的名称'
                }, {
                    iconCls: 'settings #eee'
                }
            ]
        },
        {
            cls: 'personaltop',
            items: [{
                //iconCls: 'trash #eee',
                html: '我的优惠券'
            },
            {
                //iconCls: 'maps #eee',
                html: '我的银行卡'
            },
            {
                //iconCls: 'star #eee',
                html: '我的收藏'
            }]
        }]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    }
})