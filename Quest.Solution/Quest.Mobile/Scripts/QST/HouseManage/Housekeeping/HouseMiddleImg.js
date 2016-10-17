/**-----------------------------------------------------------------
* @explanation:家政服务头部滚动图片
* @created：XS
* @create time：2015/1/27 
* @modified history: //修改历史
/-------------------------------------------------------------------*/
Ext.define('QST.HouseManage.Housekeeping.HouseMiddleImg', {
    extend: 'Ext.Container',
    xtype: 'house_middle_img',
    alternateClassName: 'house_middle_img',
    config: {
        layout: 'vbox',
        height: 180,
        cls: 'banner',
        defaults: {
            flex: 1,
            layout: {
                type: 'hbox',
                align: 'top'
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

                }, {
                    iconCls: 'settings #eee'
                }
            ]
        }]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);
    }
});
