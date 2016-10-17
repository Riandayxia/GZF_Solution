/**-----------------------------------------------------------------
* @explanation:数据字典信息编辑
* @created：rainday
* @create time：2016-08-19 09:35
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("Base.Dictionary.Edit", {
    extend: 'Ext.user.NEdit',
	xtype: 'base_dictionary_edit',
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
        { xtype: 'hiddenfield', fieldLabel: '父级Id', name: 'ParentId'},
        { xtype: 'textfield', fieldLabel: '关键字', name: 'Keyword'},
        { xtype: 'textfield', fieldLabel: 'Key', name: 'Key'},
        { xtype: 'textfield', fieldLabel: 'Value', name: 'Value'},
        { xtype: 'numberfield', fieldLabel: '顺序', name: 'Sequence'},
        { xtype: 'textfield', fieldLabel: '描述', name: 'Desc'},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'radiofield', fieldLabel: '是否删除', name: 'IsDeleted'},
        { xtype: 'datefield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date()},
    ]
});
