/**-----------------------------------------------------------------
* @explanation:自定义列表布局
* @created：rainday
* @create time：2016-09-01 13:05
* @modified history: //修改历史
/-------------------------------------------------------------------*/

Ext.define('BPM.CDList.Layout', {
    extend: 'Ext.panel.Panel',
    xtype: 'bpm_cdlist_layout',
    layout: 'fit',
    // 初始化
    initComponent: function () {
        var me = this;
        me.resetData();

        me.dockedItems = [{
            xtype: 'toolbar',
            dock: 'top',
            cls: 'showFrom',
            items: [{
                text: '打开', iconCls: 'wficon_openFlow', handler: function (but) {
                    me.openList();
                }
            }, {
                text: '新建', iconCls: 'wficon_addFlow', handler: function (but) {
                    me.addList();
                }
            }, {
                text: '功能', iconCls: 'wficon_sitemap', handler: function (but) {
                    me.setFeatur();
                }
            }, {
                text: '纵列', iconCls: 'wficon_setting', handler: function (but) {
                    me.setColumnAttr();
                }
            }, {
                text: '属性', iconCls: 'wficon_flowAttr', handler: function (but) {
                    me.attrList();
                }
            }, {
                text: '保存', iconCls: 'wficon_saveFlow', handler: function (but) {
                    me.saveList();
                }
            }, {
                text: '发布', iconCls: 'wficon_world', handler: function (but) {
                    me.putList();
                }
            }]
        }];
        this.callParent(arguments);
    },
    // 重置本地化数据
    resetData: function () {
        var me = this;
        me.list_data = {};// 列表数据
        me.list_data.columns = [];//列表纵列数组
        me.list_data.modelArray = [];//列表模型字段数组
        me.list_data.listeners = {};//列表事件
        me.list_data.featureArray = [];//列表功能集合
        me.list_data.add_Data = function () { };//添加方法
        me.list_data.updata_Data = function () { };//修改
        me.list_data.del_Data = function () { };//删除
        me.list_data.search_Data = function () { };//查询
    },
    // 打开列表清单
    openList: function () {
        var me = this;
        if (!me.openGrid) {
            //获得 添加窗体对象
            var object = {
                winTitle: '列表集合', winWidth: 660, winHeight: 300, win: 'BPM.CDList.Grid', config: {
                    // 事件听器
                    listeners: {
                        // 双击事件
                        itemdblclick: function (view, record, item, index, e, eOpts) {
                            view.up('window').hide();
                            var data = Ext.decode(record.data.DesignJson);
                            Ext.apply(data, {
                                Id: record.data.Id
                            });
                            me.list_data = data;
                            me.setGridItems();
                        }
                    }
                }
            };
            me.openGrid = util.createWindow(object);
        }
        me.openGrid.show();
        me.openGrid.down('bpm_cdlist_grid').store.load();
    },
    // 添加列表配置
    addList: function () {
        var me = this;
        if (!me.listWin) {
            ////获得 添加窗体对象
            var object = {
                winTitle: '新建', winWidth: '660px', win: 'BPM.CDList.BasicEdit', config: {
                    //监听器
                    listeners: {
                        // 表单提交
                        formSubmit: function (but, view) {
                            but.up('window').hide();
                            var from = view.getForm();
                            me.setColumns(from.getValues());
                        }
                    }
                }
            }
            me.listWin = util.createWindow(object);
        }
        var form = me.listWin.down("form").getForm();
        form.reset();
        me.resetData();
        // 打开窗体
        me.listWin.show();
    },
    // 列表配置属性
    attrList: function () {
        var me = this;
        if (!me.listWin) {
            ////获得 添加窗体对象
            var object = {
                winTitle: '属性', winWidth: '660px', win: 'BPM.CDList.BasicEdit', config: {
                    //监听器
                    listeners: {
                        // 表单提交
                        formSubmit: function (but, view) {
                            but.up('window').hide();
                            var from = view.getForm();
                            me.setColumns(from.getValues());
                        }
                    }
                }
            }
            me.listWin = util.createWindow(object);
        }
        var form = me.listWin.down("form").getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(me.list_data);
        // 打开窗体
        me.listWin.show();
    },
    // 保存列表配置
    saveList: function () {
        var me = this;
        var data = {};
        Ext.apply(data, me.list_data);
        data.DesignJson = Ext.encode(me.list_data)
        util.request({
            url: 'CDList/AddOrUpdate',
            params: data,//参数
            method: 'POST',
            async: false,
            success: function (result) {
                if (result.success) {
                    util.msgTip(result.msg);
                }
            }
        });
    },
    // 发布列表配置
    putList: function () {
        var me = this;
        var data = {};
        Ext.apply(data, me.list_data);
        data.DesignJson = Ext.encode(me.list_data);
        data.RunJson = data.DesignJson;
        util.request({
            url: 'CDList/AddOrUpdate',
            params: data,//参数
            method: 'POST',
            async: false,
            success: function (result) {
                if (result.success) {
                    util.msgTip('发布成功!');
                }
            }
        });
    },
    // 设置列表
    setColumns: function (data) {
        var me = this;
        me.resetData();
        Ext.apply(me.list_data, data);

        util.request({
            url: 'CDColumn/GetCombox',
            params: { tId: data.DBTableId },//参数
            method: 'POST',
            async: false,
            success: function (result) {
                if (result.success) {
                    result.data.forEach(function (item, index) {

                        me.list_data.columns.push({
                            dataIndex: item.Value,
                            header: item.Text,
                            tobject: item
                        });
                        me.list_data.modelArray.push(item.Value);

                        Ext.apply(me.list_data, {
                            // 功能列表
                            featureArray: [{
                                action: 'add_Data',
                                text: '新增',
                                serveUrl: data.DBTableName + '/Add',
                                iconCls: 'icon_add',
                                type: [1, 3]
                            }, {
                                action: 'updata_Data',
                                text: '修改',
                                serveUrl: data.DBTableName + '/Update',
                                iconCls: 'icon_edit',
                                type: [1, 2]
                            }, {
                                action: 'del_Data',
                                text: '删除',
                                serveUrl: data.DBTableName + '/Delete',
                                iconCls: 'icon_delete',
                                type: [1, 2]
                            }, {
                                action: 'search_Data',
                                otype: 'nsearch',
                                serveUrl: data.DBTableName + '/Search',
                                width: 300,
                                type: [1]
                            }],
                            //列表事件
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
                                alert('添加')
                            },
                            // 修改
                            updata_Data: function (but, record, grid) {
                                var me = this;
                                alert('修改')
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
                                            grid.store.load();
                                        }
                                    }
                                });
                            },
                            // 查询
                            search_Data: function (but, record, grid) {
                                alert('查询')
                            }
                        })
                        me.setGridItems();
                    });
                }
            }
        });
    },
    // 设置grid预览效果
    setGridItems: function (config) {
        var me = this;
        if (!config)
            config = {};

        Ext.apply(config, me.list_data);
        config.al = true;
        me.grid = Ext.create('Ext.user.NGrid', config);
        me.items.removeAll();
        me.add(me.grid);
    },
    // 设置Grid头部功能
    setFeatur: function () {
        var me = this;
    },
    // 设置列表属性
    setColumnAttr: function () {
        var me = this;
    }
});
