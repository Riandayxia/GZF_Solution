/**-----------------------------------------------------------------
* @explanation:任务信息信息编辑
* @created：rainday
* @create time：2016-08-19 09:35
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表编辑页面
*/
Ext.define("BPM.WFTask.Edit", {
    extend: 'Ext.user.NEdit',
    xtype: 'bpm_wftask_edit',
    items: [
        {
            xtype: 'panel', name: 'FromHtml', layout: 'fit', cls: 'ueFrom', width: '100%'
        },
        {
            xtype: 'panel', layout: 'vbox', width: '100%', cls: 'task',
            defaults: {
                labelAlign: 'right',
            },
            items: [
                { xtype: 'hiddenfield', fieldLabel: '上一任务Id', name: 'ParentId' },
                { xtype: 'hiddenfield', fieldLabel: '实体Id', name: 'InstanceId' },
                { xtype: 'hiddenfield', fieldLabel: '主体id', name: 'MainId' },
                { xtype: 'hiddenfield', fieldLabel: '步骤Id', name: 'StepId' },
                { xtype: 'hiddenfield', fieldLabel: '表单Id', name: 'FormId' },
                { xtype: 'hiddenfield', fieldLabel: '任务类型', name: 'Type' },
                { xtype: 'hiddenfield', fieldLabel: '发送人', name: 'SenderId' },
                { xtype: 'hiddenfield', fieldLabel: '步骤名称', name: 'StepName' },
                { xtype: 'hiddenfield', fieldLabel: '任务执行人', name: 'ReceiveId' },
                { xtype: 'hiddenfield', fieldLabel: '任务地址', name: 'FormUrl' },
                { xtype: 'hiddenfield', fieldLabel: '标题', name: 'Title' },
                { xtype: 'hiddenfield', fieldLabel: '其它说明', name: 'Note' },
                { xtype: 'hiddenfield', fieldLabel: '打开时间', name: 'OpenTime', dateFormat: 'Y/m/d ', value: new Date() },
                { xtype: 'hiddenfield', fieldLabel: '规定完成时间', name: 'CompletedTime', dateFormat: 'Y/m/d ', value: new Date() },
                { xtype: 'hiddenfield', fieldLabel: '实际完成时间', name: 'ActualFinishTime', dateFormat: 'Y/m/d ', value: new Date() },
                { xtype: 'hiddenfield', fieldLabel: '是否签章', name: 'IsSign' },
                { xtype: 'hiddenfield', fieldLabel: '状态', name: 'Status' },
                { xtype: 'hiddenfield', fieldLabel: '序号', name: 'Sort' },
                { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id' },
                { xtype: 'hiddenfield', fieldLabel: '是否删除', name: 'IsDeleted' },
                { xtype: 'hiddenfield', fieldLabel: '创建时间', name: 'CreatedTime', dateFormat: 'Y/m/d ', value: new Date() },
                { xtype: 'hiddenfield', fieldLabel: '修改时间', name: 'LastUpdatedTime', dateFormat: 'Y/m/d ', value: new Date() },
                {
                    xtype: 'textareafield',
                    //grow: true,
                    name: 'Comment',
                    fieldLabel: '意见',
                    width: '100%'
                    //anchor: '100%'
                },
                {
                    xtype: 'fieldcontainer',
                    fieldLabel: '是否同意',
                    defaultType: 'radiofield',
                    layout: 'hbox',
                    items: [
                        {
                            boxLabel: '同意',
                            margin: '0 20 0 0',
                            name: 'Agreest',
                            inputValue: 'true',
                            id: 'radio1'
                        }, {
                            boxLabel: '不同意',
                            name: 'Agreest',
                            inputValue: 'false',
                            id: 'radio2'
                        }
                    ]
                }
            ]
        },
    ]
});
