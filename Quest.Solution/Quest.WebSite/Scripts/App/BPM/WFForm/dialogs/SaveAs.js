/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.SaveAs", {
    extend: 'Ext.user.NEdit',
    xtype: 'saveas_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    closeAction: 'hide',
    layout: {
        type: 'table',
        columns: 1
    },
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
      
        { fieldLabel: '表单名称', name: 'Height', xtype: 'textfield' },
      
    ]
});