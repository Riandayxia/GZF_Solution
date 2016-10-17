/**-----------------------------------------------------------------
* @explanation:自定义列表纵列信息编辑
* @created：rainday
* @create time：2016-09-01 13:05
* @modified history: //修改历史
/-------------------------------------------------------------------*/

/*
* 数据表纵列信息编辑页面
*/
Ext.define("BPM.CDList.Columns", {
    extend: 'Ext.user.NGrid',
    xtype: 'bpm_cdlist_columns',
    //border: '1px',
    //style: 'border: 1px dashed #157fcc !important;',
    modelArray: ['dataIndex', 'header', 'width', 'hideable', 'sortable', 'Other'],
    columns: [
        { header: '数据索引', dataIndex: 'dataIndex', flex: 1 },
        { header: '列标头', dataIndex: 'header', flex: 1 },
        { header: '宽度', dataIndex: 'width', flex: 1 },
        { header: '是否隐藏', dataIndex: 'hideable', flex: 1 },
        { header: '是否排序', dataIndex: 'sortable', flex: 1 },
        { header: 'Config', dataIndex: 'Other', flex: 1 },
    ],
    featureArray: [{
        action: 'add_Data',
        iconCls: 'icon_add',
        text: '新增',
        type: [3]
    }, {
        action: 'updata_Data',
        iconCls: 'icon_edit',
        text: '修改',
        type: [2]
    }, {
        action: 'del_Data',
        iconCls: 'icon_delete',
        text: '删除',
        type: [2]
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
        }
    },// 添加
    add_Data: function (but, record, grid) {
        var me = this;
        //获得 添加窗体对象
        var object = {
            winTitle: but.text, winWidth: 660, win: 'BPM.CDList.ColumnEdit', hideFields: me.typeFields, config: {
                formSubmit: false,
                formSubmit: function (action) {
                    alert();
                }
            }
        }
        if (!grid.winEdit) {
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
            winTitle: but.text, winWidth: 660, win: 'BPM.CDList.ColumnEdit', hideFields: me.typeFields, config: {
                formSubmit: false,
                formSubmit: function () {
                    alert();
                }
            }
        }
        if (!grid.winEdit) {
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





    },
    // 加载数据
    load_Data: function () {
        this.store.load();
    },
});
