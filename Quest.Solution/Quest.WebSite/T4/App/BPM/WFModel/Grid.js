/**-----------------------------------------------------------------
* @explanation:流程模型信息列表
* @created：rainday
* @create time：2016-09-22 09:17
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('BPM.WFModel.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'bpm_wfmodel_grid',
    stateId: 'bpm_wfmodel_grid',
    controllerName: 'WFModel',
    al: false,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'WFModel/GetAll',
    modelArray: ['Name', 'DBTableName', 'DBFieldName', 'Manager', 'InstanceManager', 'CreateUserID', 'DesignJSON', 'InstallDate', 'InstallUserID', 'Status', 'Desc', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '流程名称', dataIndex: 'Name', flex: 1 },
		{ text: '表名称', dataIndex: 'DBTableName', flex: 1 },
		{ text: '完成标识', dataIndex: 'DBFieldName', flex: 1 },
		{ text: '管理人员', dataIndex: 'Manager', flex: 1 },
		{ text: '实例管理人员', dataIndex: 'InstanceManager', flex: 1 },
		{ text: '创建人', dataIndex: 'CreateUserID', flex: 1 },
		{ text: '设计Json', dataIndex: 'DesignJSON', flex: 1 },
		{ text: '安装日期', dataIndex: 'InstallDate', flex: 1 },
		{ text: '安装人员', dataIndex: 'InstallUserID', flex: 1 },
		{ text: '状态', dataIndex: 'Status', flex: 1 },
		{ text: '描述', dataIndex: 'Desc', flex: 1 },
		{ text: '唯一标识', dataIndex: 'Id', flex: 1 },
		{ text: '是否删除', dataIndex: 'IsDeleted', flex: 1 },
		{ text: '创建时间', dataIndex: 'CreatedTime', flex: 1 },
		{ text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1 },

    ],
    featureArray: [{
        action: 'add_Data',
        iconCls: 'icon_add',
        text: '新增',
        serveUrl: 'WFModel/Add',
        type: [1, 3]
    }, {
        action: 'updata_Data',
        iconCls: 'icon_edit',
        text: '修改',
        serveUrl: 'WFModel/Update',
        type: [1, 2]
    }, {
        action: 'del_Data',
        iconCls: 'icon_delete',
        text: '删除',
        serveUrl: 'WFModel/Delete',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        action: 'search_Data',
        serveUrl: 'WFModel/Search',
        width: 300,
        type: [1]
    }],
    // 事件监听器
    listeners: {
        // 添加
        add_Data: function (but, record) {
            this.add_Data(but, record, this);
        },
        // 修改
        updata_Data: function (but, record) {
            this.updata_Data(but, record, this);
        },
        // 删除
        del_Data: function (but, record) {
            this.del_Data(but, record, this);
        },
        // 查询
        search_Data: function (but, record) {
            this.search_Data(but, record, this);
        }
    },
    // 添加
    add_Data: function (but, record, grid) {
        var me = this;
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.WFModel.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function (action) {
                    if (action.result.success) {
                        grid.load_Data();
                    }
                }
            }
        }
        if(!grid.winEdit){
            grid.winEdit = util.createWindow(object);
        }
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        // 打开窗体
        grid.winEdit.show();
    },
    // 修改
    updata_Data: function (but, record, grid) {
        var me = this;
        //得到选中的数据
        var selected = grid.getSelectionTobject();
        if (!selected) {
            Ext.Msg.alert("提示", "请选择修改数据");
            return;
        }
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.WFModel.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function () {
                    grid.load_Data();
                }
            }
        }
        if(!grid.winEdit){
            grid.winEdit = util.createWindow(object);
        }
        var form = grid.winEdit.down("form").getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(selected);
        // 打开窗体
        grid.winEdit.show();
    },
    // 删除
    del_Data: function (but, record, grid) {
        var me = this;
        //得到选中的数据
        var selected = grid.getSelectionTobject();
        if (!selected) {
            Ext.Msg.alert("提示", "请选择删除数据");
            return;
        }
        util.doDelete({
            url: but.menuObj.serveUrl,//提交地址
            params: { ids: selected.Id },//参数
            success: function (result) {
                if (result) {
                    grid.load_Data();
                }
            }
        });
    },
    // 加载数据
    load_Data: function () {
        this.store.load();
    },
    // 查询
    search_Data: function (but, record, grid) { }
});
