Ext.define('QST.Personal.Layout', {
    extend: 'Ext.Container',
    xtype: 'personal_layout',
    alternateClassName: 'personal',
    requires: ['QST.Personal.Top', 'QST.Personal.Middle', 'QST.Personal.Bottom'],
    config: {
        scrollable: {
            directionLock: true,
            //注意横向竖向模式的配置，不能配错  
            direction: 'vertical',
            //隐藏滚动条样式  
            indicators: false
        },
        items: [{
            xtype: 'personal_top'
        }, {
            xtype: 'personal_middle'
        }, {
            xtype: 'personal_bottom'
        }]
    },
    //初始化
    constructor: function (cfg) {
        var me = this;
        this.callParent(arguments);

    }
})