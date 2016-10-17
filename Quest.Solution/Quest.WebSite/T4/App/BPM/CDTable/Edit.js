/**-----------------------------------------------------------------
* @explanation:数据表信息编辑
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.CDTable.Edit", {
    extend: 'Ext.user.NEdit',
	xtype: 'bpm_cdtable_edit',
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
        { xtype: 'textfield', fieldLabel: '数据库表名', name: 'Name'},
        { xtype: 'textfield', fieldLabel: '别名', name: 'ByName'},
        { xtype: 'textfield', fieldLabel: '描述', name: 'Desc'},
        { xtype: 'numberfield', fieldLabel: '排序', name: 'Sort'},
        { xtype: 'textfield', fieldLabel: '创建者', name: 'Creator'},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'radiofield', fieldLabel: '是否删除', name: 'IsDeleted'},
        { xtype: 'datefield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date()},
    ]
});
