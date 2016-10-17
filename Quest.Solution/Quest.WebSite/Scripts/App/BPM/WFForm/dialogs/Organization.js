/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.Organization", {
    //extend: 'Ext.user.NEdit',
    extend: 'Ext.user.NEdit',
    xtype: 'organization_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    closeAction: 'hide',
    items: [{
        layout: {
            type: 'table',
            columns: 1
        },
        defaults: {
            labelAlign: 'right',
            width: 450
        },
        items: [
             { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
             { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
             { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
             { fieldLabel: '绑定字段', name: 'Width', xtype: 'combobox' },
             {
                 fieldLabel: '默认值', name: 'Height', xtype: 'fieldcontainer',
                 layout: 'hbox',
                 items: [
                     {
                         xtype: 'textfield',
                     }, {
                         xtype: 'combobox',
                        
                     }
                 ]
             },
             { fieldLabel: '宽度', name: 'Width', xtype: 'textfield' },
             {
                 fieldLabel: '选择范围', xtype: 'fieldcontainer',
                 width: 300,
                 bodyPadding: 10,
                 items: [{
                     defaultType: 'radiofield',
                     layout: 'hbox',
                     defaults: {
                         flex: 1
                     },
                     items: [
                         {
                             boxLabel: '发起者部门',
                         },
                         {
                             boxLabel: '处理者部门',

                         }
                     ]
                 }, {
                     layout: 'hbox',
                     items: [
                         {
                             xtype: 'radiofield',
                             boxLabel: '自定义:',
                         },
                         {
                             margin: '0 0 0 10',
                             xtype: 'combobox',
                             width: 135
                         }
                     ]
                 } 
                 ]
             },
             {
                 fieldLabel: '选择类型', name: 'Height', xtype: 'fieldcontainer',
                 defaultType: 'checkboxfield',
                 layout: 'hbox',
                 colspan: 2,
                 defaults: {
                     flex: 1
                 },
                 items: [
                     {
                         boxLabel: '部门',
                     },
                     {
                         boxLabel: '岗位',
                       
                     },
                     {
                         boxLabel: '人员',
                     },
                     {
                         boxLabel: '工作组',

                     },
                     {
                         boxLabel: '单位',
                     }
                 ]
             },
             { fieldLabel: '多选', name: 'Width', boxLabel: '是否可以多选', xtype: 'checkboxfield', margin: '0 0 30 0' },

             { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
             { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
             { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
        ]
    }],
});