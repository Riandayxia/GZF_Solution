/*
工程管理布局
*/
Ext.define('BPM.CDColumn.Layout', {
    extend: 'Ext.container.Container',
    xtype: 'main_layout',
    layout: 'border',
    requires: ['BPM.CDColumn.Grid'],
    items: [{
        region: 'center', xtype: 'dbcolumn_auto_grid', collapsible: true, split: true, header: false
    }, {
        region: 'south', xtype: 'dbcolumn_auto_grid', collapsible: true, split: true, header: false, height: '50%'
    }],
    initComponent: function () {
        var me = this;
        this.callParent(arguments);
    }
});
