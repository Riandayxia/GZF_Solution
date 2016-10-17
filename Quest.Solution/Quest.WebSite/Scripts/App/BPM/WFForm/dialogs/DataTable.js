/*
* 表单属性
*/
Ext.define("BPM.WFForm.dialogs.DataTable", {
    extend: 'Ext.user.NEdit',
    xtype: 'datatable_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    closeAction: 'hide',
    layout: {
        type: 'table',
        columns: 2
    },
    defaults: {
        labelAlign: 'right',
        width: 400,
        height:50,
        flex: 1
    },
    items: [
        { fieldLabel: '编号', name: 'Id', xtype: 'hiddenfield' },
        { fieldLabel: '模板Id', name: 'TempleId', xtype: 'hiddenfield' },
        { fieldLabel: '审批', name: 'SortOrders', xtype: 'hiddenfield' },
        {
            fieldLabel: '样式', name: 'Title', xtype: 'fieldcontainer', colspan: 2,
            defaultType: 'textfield',
            layout: 'hbox',
            items: [
                  {
                      xtype: 'textfield',
                      name: 'name',
                      fieldLabel: '宽度',
                      labelWidth: 30,
                      width:150
                  },
                  {
                      xtype: 'textfield',
                      name: 'name',
                      fieldLabel: '高度',
                      labelWidth: 30,
                      width: 150
                  }
            ]
        },

        {
            fieldLabel: '数据类型', name: 'Height', xtype: 'fieldcontainer',
            defaultType: 'radiofield',
            layout: 'hbox',
            colspan: 2,
            defaults: {
                flex: 1
            },
            items: [
                {
                    boxLabel: 'DataTable',
                    name: 'radiofield',
                    inputValue: 'm',
                },
                {
                    boxLabel: 'HTML',
                    name: 'radiofield',
                    inputValue: 'l',
                },
                {
                    boxLabel: 'JSON',
                    name: 'radiofield',
                    inputValue: 'xl',
                }
            ]
        },

        {
            fieldLabel: '数据来源', name: 'Height', xtype: 'fieldcontainer',
            defaultType: 'radiofield', colspan: 2,
            layout: 'hbox',
            defaults: {
                flex: 1
            },
            items: [
                {
                    boxLabel: 'SQL',
                    name: 'radiofield',
                    inputValue: 'm',
                },
                {
                    boxLabel: 'URL',
                    name: 'radiofield',
                    inputValue: 'l',
                },
                {
                    boxLabel: '方法',
                    name: 'radiofield',
                    inputValue: 'xl',
                }
            ]
        },
        { fieldLabel: '是否', name: 'IsDeleted', xtype: 'hiddenfield' },
        { fieldLabel: '创建时间', name: 'CreatedTime', xtype: 'hiddenfield' },
        { fieldLabel: '修改时间', name: 'LastUpdatedTime', xtype: 'hiddenfield' },
    ]
});