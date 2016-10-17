/*
* 数据表编辑页面
*/
Ext.define("Compiler.HelloWorld.Auto.Edit", {
    extend: 'Ext.user.NEdit',
    xtype: 'dbtable_auto_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    layout: {
        type: 'table',
        columns: 1
    },
    closeAction: 'hide',
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '表名称', name: 'Name', xtype: 'textfield', allowBlank: false },
        { fieldLabel: '创建者', name: 'Creator', xtype: 'textfield', allowBlank: false },
        { fieldLabel: '描述', name: 'Desc', xtype: 'textarea', colspan: 2, width: 600 },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield', },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ]
});