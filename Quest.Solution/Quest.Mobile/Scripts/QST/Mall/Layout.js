/**-----------------------------------------------------------------
* @explanation:购物中心
* @created：Rainday
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.Mall.Layout', {
    extend: 'Ext.Container',
    xtype: 'mall_layout',
    requires: ['QST.Mall.Menu', 'QST.Mall.Top', 'QST.Mall.MiddleImg', 'QST.Mall.Selling', 'QST.Mall.Digital'],
    fullscreen: true,
    config: {
        layout: 'vbox',
        scrollable: {
            directionLock: true,
            //注意横向竖向模式的配置，不能配错  
            direction: 'vertical',
            //隐藏滚动条样式  
            indicators: false
        },
        items: [{
            xtype: 'mall_top'
        }, {
            xtype: 'panel',
            html: '<img src="resources/images/mallTop.png" class="topImg" fire="onDelete">'
        }, {
            xtype: 'mall_menu'
        }, {
            xtype: 'mall_middle_img'
        }, {
            xtype: 'mall_selling'
        }, {
            xtype: 'mall_digital'
        }
        ]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    },
});
