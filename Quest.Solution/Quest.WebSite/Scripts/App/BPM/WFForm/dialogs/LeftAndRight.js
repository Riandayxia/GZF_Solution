/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.LeftAndRight", {
    //extend: 'Ext.user.NEdit',
    extend: 'Ext.user.NEdit',
    xtype: 'leftandright_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    closeAction: 'hide',
    items: [{
        layout: {
            type: 'table',
            columns: 2
        },
        defaults: {
            labelAlign: 'right',
            width: 600
        },
        items: [
             { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
             { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
             { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
             { fieldLabel: '绑定字段', name: 'Width', xtype: 'combobox', width: 550, colspan: 2, },
             { fieldLabel: '宽度', name: 'Height', xtype: 'textfield', width: 250 },
             { fieldLabel: '选择框标题', name: 'Height', xtype: 'textfield', width: 250 },
             {
                 fieldLabel: '选择控制', name: 'Height', xtype: 'fieldcontainer',
                 defaultType: 'checkboxfield',
                 layout: 'hbox',
                 colspan: 2,
                 defaults: {
                     flex: 1
                 },
                 items: [
                     {
                         boxLabel: '是否允许多选',
                     },
                     {
                         labelWidth: 10,
                         boxLabel: '是否运行选择根节点',

                     },
                     {
                         //margin: '0 0 0 60',
                         boxLabel: '是否允许选择父节点',
                     }
                 ]
             },
             {
                 fieldLabel: '数据来源', name: 'Height', xtype: 'fieldcontainer',
                 defaultType: 'radiofield',
                 layout: 'hbox',
                 colspan: 2,
                 defaults: {
                     flex: 1
                 },
                 items: [
                     {
                         boxLabel: '数据字典',
                     },
                     {
                         boxLabel: 'SQL',

                     },
                     {
                         boxLabel: 'URL',
                     },
                     {
                         boxLabel: '数据表',
                     }
                 ]
             },
             {
                 fieldLabel: '字典根', name: 'Height', xtype: 'fieldcontainer',
                 layout: 'hbox',
                 colspan: 2,
                 defaults: {
                     flex: 1
                 },
                 items: [
                     {
                         xtype: 'combobox',
                     },
                     {
                         xtype: 'checkboxfield',
                         boxLabel: '是否加载所有下级节点',
                         margin: '0 0 0 30'
                     }
                 ]
             },
             { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
             { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
             { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
        ]
    }],
});