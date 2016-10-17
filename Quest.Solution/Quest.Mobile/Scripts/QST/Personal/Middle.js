Ext.define('QST.Personal.Middle', {
    extend: 'Ext.Container',
    xtype: 'personal_middle',
    alternateClassName: 'personal_middle',
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
                    html: '我的订单'
                }, {
                    align: 'right',
                    html: '查看全部订单<img class="personalArrow" fire="onDelete" src="resources/images/Arrow.png">'
                }]
            },
            {
                layout: 'vbox',
                cls: 'personalOrder',
                defaults: {
                    layout: 'hbox',
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
                                text: '待付款',
                                iconCls: 'daifukuan'
                            }, {
                                text: '待发货',
                                iconCls: 'daifahuo'
                            }, {
                                text: '待收货',
                                iconCls: 'daishouhuo'
                            }, {
                                text: '待评论',
                                iconCls: 'daipingjia'
                            }, {
                                text: '退款/售后',
                                iconCls: 'tuikuanshouhou'
                            }
                       ]
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