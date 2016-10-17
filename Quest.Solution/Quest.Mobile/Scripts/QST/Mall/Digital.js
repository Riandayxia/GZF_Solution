Ext.define('QST.Mall.Digital', {
    extend: 'Ext.Container',
    xtype: 'mall_digital',
    alternateClassName: 'mall_digital',
    config: {
        layout: 'vbox',
        style: 'border:1px solid #CCC',
        defaults: {
            layout: 'hbox',
            defaults: {
                flex: 1,
                xtype: 'label',
                iconAlign: 'top',
                style: 'margin-left:.5em;',
            }
        },
        items: [
            {
                xtype: 'titlebar',
                docked: 'top',
                cls: 'personalBar',
                defaults: {
                    xtype: 'label',
                },
                items: [{
                    align: 'left',
                    html: '数码电器'
                }, {
                    align: 'right',
                    html: '更多<img class="personalArrow" fire="onDelete" src="resources/images/Arrow.png">'
                }]
            },
            {
                layout: 'vbox',
                cls: 'personalOrder',
                defaults: {
                    layout: 'hbox',
                },
                items: [
                   {
                       html:'商品列表'
                   }
                ]
            }
        ]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);

    }
})