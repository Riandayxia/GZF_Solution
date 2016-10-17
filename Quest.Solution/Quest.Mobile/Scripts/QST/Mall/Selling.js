/**-----------------------------------------------------------------
* @explanation:热销
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Mall.Selling', {
    extend: 'Ext.Container',
    xtype: 'mall_selling',
    config: {
        layout: 'vbox',
        //height: 150,
        cls: 'home',
        style: 'margin: 5px 15px;',
        defaults: {
            layout: 'hbox',
            defaults: {
                flex: 1,
                xtype: 'button',
                iconAlign: 'bottom'
            }
        },
        items: [
            {
                items: [{
                    html: '<div><span style="font-size:15px;font-weight: bold;">电器节<span style="color: #00BBFF">打折</span></span><img style:"margin: -6px 6px;" src="resources/images/Mall/duobianxing1@2x.png" width="20%" height="20%"></div><div style="text-align:center"><img src="resources/images/Mall/dianqi@3x.png" width="100%" height="150%"></div><div style="text-align:center"></div>',
                    style: {
                        'text-align': 'left',
                        'color': ' #666',
                        'font-size': '.8em'
                    },
                }, {
                    html: '<div><span style="font-size:15px;font-weight: bold;">红米国庆<span style="color: #CCFF99">特价</span></span><img style:"margin: -6px 6px;" src="resources/images/Mall/duobianxing2@2x.png" width="20%" height="20%"></div><div style="text-align:center"><img src="resources/images/Mall/shouji@3x.png" width="100%" height="150%"></div><div style="text-align:center"></div>',
                    style: {
                        'text-align': 'left',
                        'color': ' #666',
                        'font-size': '.8em'
                    },
                }, {
                    html: '<div><span style="font-size:15px;font-weight: bold;">新西兰<span style="color: #FF88C2">苹果</span></span><img style:"margin: -6px 6px;" src="resources/images/Mall/duobianxing3@2x.png" width="20%" height="20%"></div><div style="text-align:right"><img src="resources/images/Mall/shuiguolan@3x.png" width="100%" height="150%"></div><div style="text-align:center"></div>',
                    style: {
                        'text-align': 'right',
                        'color': ' #666',
                        'font-size': '.8em'
                    },
                }]
            }
        ],
        listeners: {
        }
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    }
});
