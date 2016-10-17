/**-----------------------------------------------------------------
* @explanation:自定义列表纵列信息编辑
* @created：rainday
* @create time：2016-09-01 13:05
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表纵列信息编辑页面
*/
Ext.define("BPM.CDList.BasicEdit", {
    extend: 'Ext.user.NEdit',
    xtype: 'bpm_cdlist_basicedit',
    layout: 'fit',
    modal: true,
    onlySave: true,
    formSubmit: false,
    layout: {
        type: 'table',
        columns: 2
    },
    closeAction: 'hide',
    defaults: {
        labelAlign: 'right',
        width: 300
    },
    items: [
        { xtype: 'hiddenfield', fieldLabel: '唯一标识', name: 'Id' },
        { xtype: 'hiddenfield', fieldLabel: '数据库名称', name: 'DBTableName' },
        { xtype: 'hiddenfield', fieldLabel: 'DesignJson', name: 'DesignJson' },
        { xtype: 'hiddenfield', fieldLabel: 'RunJson', name: 'RunJson' },
        { xtype: 'ndiction', fieldLabel: '绑定数据表', name: 'DBTableId', dataUrl: '/CDTable/GetCombox', dicKey: 'CDListTableId' },
        { xtype: 'textfield', fieldLabel: '标题', name: 'title' },
        { xtype: 'textfield', fieldLabel: '创建者', name: 'Creator' },
        { xtype: 'textfield', fieldLabel: '选择框', name: 'isSelect', value: false },
        { xtype: 'textfield', fieldLabel: '自动加载', name: 'al', value: true },
        { xtype: 'numberfield', fieldLabel: '高度', name: 'width' },
        { xtype: 'numberfield', fieldLabel: '宽度', name: 'height' },
        { xtype: 'textfield', fieldLabel: '数据地址', name: 'dataUrl' },
        { xtype: 'textarea', fieldLabel: '其他配置', name: 'otherConfig' },
    ],
    initComponent: function () {
        var me = this;
        me.callParent(arguments);

        var tableIdBox = me.down('[name="DBTableId"]');// 字段数据表注册下来事件
        var from = me.getForm();
        var titleTxt = me.down('[name="title"]');// 标题
        var tableNamehid = me.down('[name="DBTableName"]');// 标题
        tableIdBox.addListener('select', function (combo, records, eOpts) {
            var data=combo.getTobject();
            from.setValues({ title: data.ByName, DBTableName: data.Name, dataUrl: data.Name + '/GetAll' })
        });
    }
});
