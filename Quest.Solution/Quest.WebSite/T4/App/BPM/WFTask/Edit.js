/**-----------------------------------------------------------------
* @explanation:任务信息信息编辑
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.WFTask.Edit", {
    extend: 'Ext.user.NEdit',
	xtype: 'bpm_wftask_edit',
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
        { xtype: 'hiddenfield', fieldLabel: '上一任务Id', name: 'ParentId'},
        { xtype: 'hiddenfield', fieldLabel: '实体Id', name: 'InstanceId'},
        { xtype: 'textfield', fieldLabel: '主体id', name: 'MainId'},
        { xtype: 'hiddenfield', fieldLabel: '步骤Id', name: 'StepId'},
        { xtype: 'hiddenfield', fieldLabel: '表单Id', name: 'FormId'},
        { xtype: 'textfield', fieldLabel: '步骤名称', name: 'StepName'},
        { xtype: 'numberfield', fieldLabel: '任务类型', name: 'Type'},
        { xtype: 'textfield', fieldLabel: '标题', name: 'Title'},
        { xtype: 'hiddenfield', fieldLabel: '发送人', name: 'SenderId'},
        { xtype: 'textfield', fieldLabel: '任务执行人', name: 'ReceiveId'},
        { xtype: 'textfield', fieldLabel: '表单地址', name: 'FormUrl'},
        { xtype: 'datefield', fieldLabel: '打开时间', name: 'OpenTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'numberfield', fieldLabel: '规定完成时间', name: 'CompletedTime'},
        { xtype: 'datefield', fieldLabel: '实际完成时间', name: 'ActualFinishTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'numberfield', fieldLabel: '是否同意', name: 'Agreest'},
        { xtype: 'textfield', fieldLabel: '意见', name: 'Comment'},
        { xtype: 'textfield', fieldLabel: '是否签章', name: 'IsSign'},
        { xtype: 'numberfield', fieldLabel: '状态', name: 'Status'},
        { xtype: 'textfield', fieldLabel: '其它说明', name: 'Note'},
        { xtype: 'numberfield', fieldLabel: '序号', name: 'Sort'},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'radiofield', fieldLabel: '是否删除', name: 'IsDeleted'},
        { xtype: 'datefield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date()},
        { xtype: 'datefield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date()},
    ]
});
