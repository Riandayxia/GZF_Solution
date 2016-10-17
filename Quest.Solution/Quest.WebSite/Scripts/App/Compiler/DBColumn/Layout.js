/*
工程管理布局
*/
Ext.define('Compiler.DBColumn.Layout', {
    extend: 'Ext.container.Container',
    xtype: 'main_layout',
    layout: 'border',
    requires: ['Compiler.DBColumn.Grid'],
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
