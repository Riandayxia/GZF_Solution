/**-----------------------------------------------------------------
* @explanation:菜单信息列表
* @created：rainday
* @create time：2016-08-19 09:34
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('Base.Menu.Grid', {
    extend: 'Ext.user.NGrid',
    xtype: 'base_menu_grid',
    stateId: 'base_menu_grid',
    controllerName: 'Menu',
    al: false,
    isSelect: false,
    layout: 'fit',
    //方法地址
    dataUrl: 'Menu/GetAll',
    modelArray: ['Name', 'ParentName', 'ControllerName','Params', 'FeatureName', 'MenuLink', 'PhoneLink', 'IconClass', 'MType', 'Use', 'Id', 'IsDeleted', 'CreatedTime', 'LastUpdatedTime'],
    columns: [
        { text: '菜单名称', dataIndex: 'Name', flex: 1 },
		{ text: '上级菜单名称', dataIndex: 'ParentName', flex: 1, hidden: true },
		{ text: '控制器名称', dataIndex: 'ControllerName', flex: 1, hidden: true },
		{ text: '功能名称', dataIndex: 'FeatureName', flex: 1, hidden: true },
		{ text: '菜单链接', dataIndex: 'MenuLink', flex: 1 },
		{ text: 'PhoneLink', dataIndex: 'PhoneLink', flex: 1, hidden: true },
		{ text: '图标样式', dataIndex: 'IconClass', flex: 1 },
		{ text: '菜单类型', dataIndex: 'MType', flex: 1 },
		{ text: '菜单用途', dataIndex: 'Use', flex: 1 },
		{ text: '唯一标识', dataIndex: 'Id', flex: 1, hidden: true },
		{ text: '是否删除', dataIndex: 'IsDeleted', flex: 1, hidden: true },
		{ text: '创建时间', dataIndex: 'CreatedTime', flex: 1 },
		{ text: '修改时间', dataIndex: 'LastUpdatedTime', flex: 1, hidden: true }
    ],
    featureArray: [{
        winUrl: '',
        serveUrl: 'Menu/Add',
        text: '新增',
        iconCls: 'icon_add',
        action: 'add_Data',
        type: [1, 3]
    }, {
        text: '修改',
        serveUrl: 'Menu/Update',
        iconCls: 'icon_edit',
        action: 'updata_Data',
        type: [1, 2]
    }, {
        text: '删除',
        serveUrl: 'Menu/Delete',
        iconCls: 'icon_delete',
        action: 'del_Data',
        type: [1, 2]
    }, {
        otype: 'nsearch',
        serveUrl: 'Menu/Search',
        width: 300,
        action: 'search_Data',
        type: [1]
    }],
    // 时间监听器
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
            winTitle: but.text, winWidth: 390, win: 'Base.Menu.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function (action) {
                    if (action.result.success) {
                        grid.load_Data();
                    }
                }
            }
        }
        grid.winEdit = util.createWindow(object);
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
            winTitle: but.text, winWidth: 660, win: 'Base.Menu.Edit', hideFields: me.typeFields, config: {
                saveUrl: but.menuObj.serveUrl,
                onSaveSuccess: function () {
                    grid.load_Data();
                }
            }
        }
        grid.winEdit = util.createWindow(object);
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
