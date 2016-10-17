/**-----------------------------------------------------------------
* @explanation:流程步骤信息编辑
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.WFRunStep.Edit", {
    extend: 'Ext.user.NEdit',
	xtype: 'bpm_wfrunstep_edit',
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
        { xtype: 'hiddenfield', fieldLabel: '步骤Id', name: 'SId'},
        { xtype: 'textfield', fieldLabel: '上一步骤Id', name: 'ParentId'},
        { xtype: 'textfield', fieldLabel: '步骤名称', name: 'Name'},
        { xtype: 'hiddenfield', fieldLabel: '流程运行Id', name: 'InstanceId'},
        { xtype: 'hiddenfield', fieldLabel: '发送人', name: 'SenderId'},
        { xtype: 'hiddenfield', fieldLabel: '表单Id', name: 'FormId'},
        { xtype: 'datefield', fieldLabel: '发送时间', name: 'SenderTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'textfield', fieldLabel: 'FormUrl', name: 'FormUrl'},
        { xtype: 'textfield', fieldLabel: '接收人员Id', name: 'ReceiveId'},
        { xtype: 'datefield', fieldLabel: '接收时间', name: 'ReceiveTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '有效期', name: 'Valid', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'numberfield', fieldLabel: '状态', name: 'Status'},
        { xtype: 'numberfield', fieldLabel: '序号', name: 'Sort'},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'radiofield', fieldLabel: '是否删除', name: 'IsDeleted'},
        { xtype: 'datefield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date()},
    ]
});
