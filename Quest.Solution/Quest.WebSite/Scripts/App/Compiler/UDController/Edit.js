/*
* 控制器编辑页面
*/
Ext.define("Compiler.UDController.Edit", {
    extend: 'Ext.user.NEdit',
    xtype: 'compiler_udcontroller_edit',
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
    requires: ['UX.UEditor'],
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '对应表', name: 'TableId', xtype: 'hiddenfield' },
        { fieldLabel: '代码', name: 'Code', xtype: 'ueditor', allowBlank: false , width: 650},
        { fieldLabel: '创建者', name: 'Creator', xtype: 'textfield', xtype: 'hiddenfield' },
        { fieldLabel: '描述', name: 'Desc', colspan: 2, width: 600, xtype: 'hiddenfield' },//xtype: 'textarea',
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield', },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ]
});