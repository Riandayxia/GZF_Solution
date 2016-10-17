/*
主布局界面
*/
Ext.define('Main.Layout', {
    extend: 'Ext.container.Container',
    xtype: 'main_layout',
    layout: 'border',
    requires: [
        'Main.Top',
        'Main.Bottom',
        'Main.Center',
        'Main.Menu'
    ],
    items: [{
        region: 'north',
        xtype: 'main_top'
    }, {
        region: 'west',
        xtype: 'main_menu',
        collapsible: true,
        split: true,
        minWidth: 200,
        width: "20%"
    }, {
        region: 'south',
        split: true,
        xtype: 'main_bottom'
    }, {
        region: 'center',
        xtype: 'main_center'
    }],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});
