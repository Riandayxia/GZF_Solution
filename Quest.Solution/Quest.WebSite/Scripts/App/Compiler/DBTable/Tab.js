Ext.define('Compiler.DBTable.Tab', {
    extend: 'Ext.tab.Panel',
    xtype: 'compiler_dbtable_tab',
    stateId: 'compiler_dbtable_tab',
    requires: [
       'Compiler.DBColumn.Grid',
       'Compiler.UDController.Grid'
    ],
    items: [{
        title: '数据列',
        authHeight: true,
        layout: 'fit',
        xtype: "compiler_dbcolumn_grid"
    }, {
        title: '控制器',
        authHeight: true,
        layout: 'fit',
        xtype: "compiler_udcontroller_grid"
    }]
});