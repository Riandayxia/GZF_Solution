/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.DropdownList", {
    extend: 'Ext.user.NEdit',
    xtype: 'dropdownlist_edit',
    items: [{
        xtype: 'tabpanel',
        items: [{
            title: '属性',
            margin: '30 0 0 0',
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
                     { fieldLabel: '绑定字段', name: 'Width', xtype: 'combobox', width: 405 },
                     {
                         fieldLabel: '宽度', name: 'Width', xtype: 'fieldcontainer',
                         layout: 'hbox',
                         colspan: 2,
                         defaults: {
                             flex: 1
                         },
                         items: [
                             {
                                 xtype: 'textfield',

                             },
                             {
                                 margin: '0 0 0 10',
                                 boxLabel: '是否添加空选项',
                                 xtype: 'checkboxfield',
                             }
                         ]
                     },
                     {
                         fieldLabel: '数据源', name: 'Height', xtype: 'fieldcontainer',
                         defaultType: 'radiofield',
                         layout: 'hbox',
                         colspan: 2,
                         defaults: {
                             flex: 1
                         },
                         items: [
                             {
                                 boxLabel: '数据字典',
                                 name: 'radiofield',
                                 inputValue: 'm',
                             },
                             {
                                 boxLabel: '自定义',
                                 name: 'radiofield',
                                 inputValue: 'l',
                             },
                             {
                                 boxLabel: 'SQL语句',
                                 name: 'radiofield',
                                 inputValue: 'l',
                             }
                         ]
                     },
                     { fieldLabel: '字典项', name: 'Width', xtype: 'combobox', margin: '0 0 100 0', width: 405 },
                     { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
                     { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
                     { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
                ]
            }],
            layout: 'fit',
        }, {
            title: '默认值',
            margin: '250 0 0 0',
            items: [{

            }],
            layout: 'fit',
        }, {
            title: '事件',
            margin: '10 0 0 0',
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
                     { fieldLabel: '事件', name: 'Width', xtype: 'combobox', width: 300 },
                     { fieldLabel: '事件脚本', name: 'Width', xtype: 'textarea', margin: '0 0 50 0', height: 150, value: 'function methodName(srcObj){}' },
                     { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
                     { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
                     { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
                ]
            }],
            layout: 'fit',
        }]
    }]
});