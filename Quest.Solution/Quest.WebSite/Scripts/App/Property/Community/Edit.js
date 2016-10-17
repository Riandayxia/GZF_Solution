/**-----------------------------------------------------------------
* @explanation:活动咨询信息编辑
* @created：HYF
* @create time：2016-10-12 09:35
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 活动咨询编辑页面
*/
Ext.define("Property.Community.Edit", {
    extend: 'Ext.user.NEdit',
    xtype: 'property_community_edit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    layout: {
        type: 'table',
        columns: 2
    },
    closeAction: 'hide',
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    requires: [
        'UX.Ueditor.UEditor'
    ],
    items: [
        { xtype: 'textfield', fieldLabel: '标题', name: 'Title' },
        { xtype: 'ndiction', fieldLabel: '内容类型', name: 'ContentType', dicKey: '10003' },
        {
         xtype: 'datefield', fieldLabel: '活动时间', name: 'ActivityTime', allowBlank: true, format: 'Y/m/d' },
        { xtype: 'textfield', fieldLabel: '发布人', name: 'Publisher', colspan: 2 },
        { xtype: 'ueditor', fieldLabel: '活动内容', name: 'Content', colspan: 2 ,width:495},
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id'},
        { xtype: 'hiddenfield', fieldLabel: '是否删除', name: 'IsDeleted' },
        { xtype: 'hiddenfield', fieldLabel: '创建时间', name: 'CreatedTime'},
        { xtype: 'hiddenfield', fieldLabel: '修改时间', name: 'LastUpdatedTime' },
    ]
});
