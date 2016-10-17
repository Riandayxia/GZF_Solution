Ext.define('SH.Util', {
    extend: 'app.util',
    alternateClassName: 'SHUtil',
    statics: {
        // 跟菜单数据
        RootMenu: {},
        // 格式时间
        FormatTime: function (value) {
            if (value)
                return value.substr(0, 16).replace("T", " ").replace(/-/g, "/");
            return value;
        },
        //保留两位小数
        ToFixed: function (value, number) {
            if (value)
                return value.toFixed(number)
        },
        //保留两位小数
        toFixed: function (value) {
            if (value)
                return value.toFixed(2)
        },
        //格式时间
        InterceptTime: function (value) {
            if (value)
                return value.substr(0, 10);
            return value;
        },
        //获取用户信息
        DicGetUser: function (value) {
            var user = diction.byData(value, config.url + "/User/GetById");
            return user ? user.Name : "";
        },
        // 设置登录时间,计算服务器连接有效时间
        SetTimeCount: function () {
            ++config.CurrentTime;
            t = setTimeout("SHUtil.SetTimeCount()", 1000);
        },
        //获取数据字典中的值 onclick="javaScript:window.open(\'{Path}\')"
        //获取项目部
        DicGetProject: function (value) {
            var pjroject = diction.byData(value, config.url + "/Project/GetById");
            var rStr = pjroject ? pjroject.Name : "";
            rStr = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + rStr + '</span>';
            return rStr;
        },
        //获取合同
        DicGetContract: function (value) {
            var contract = diction.byData(value, config.url + "/Contract/Get");
            var con = contract.data ? contract.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取收入计划
        DicGetIncomePlan: function (value) {
            var IncomePlan = diction.byData(value, config.url + "/IncomePlan/Get");
            var con = IncomePlan.data ? IncomePlan.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取工期计划
        DicGetSchedulePlan: function (value) {
            var SchedulePlan = diction.byData(value, config.url + "/SchedulePlan/Get");
            var con = SchedulePlan.data ? SchedulePlan.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取支出计划
        DicGetDefrayPlan: function (value) {
            var DefrayPlan = diction.byData(value, config.url + "/DefrayPlan/Get");
            var con = DefrayPlan.data ? DefrayPlan.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取台帐
        DicGetAccounting: function (value) {
            var accounting = diction.byData(value, config.url + "/ProjectAccounting/GetValue");
            var con = accounting.data ? accounting.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        // 获取流程状态
        WFState: function (val) {
            var str = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;';
            switch (val) {
                case 1:
                    str = str + 'background-color: #E66767;">草案';
                    break;
                case 2:
                    str = str + 'background-color: #6BC8FB;">已启动'
                    break;
                case 3:
                    str = str + 'background-color: #ACCC6B;">完成'
                    break;
                case 5:
                    str = str + 'background-color: #FFD700;">驳回'
                    break;
                case 6:
                    str = str + 'background-color: #FFD700;">无效'
                    break;
                default:
                    str = str + 'background-color: #CACACA;">其他';
                    break;
            }

            return str + '</span>';
        },
        // 获取处理状态
        ProcessState: function (val) {
            var str = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;';
            switch (val) {
                case false:
                    str = str + 'background-color: #E66767;">未审批';
                    break;
                case true:
                    str = str + 'background-color: #6BC8FB;">已审批'
                    break;
                default:
                    str = str + '">';
                    break;
            }

            return str + '</span>';
        }, // 获取处理状态
        WFHandleState: function (val) {
            var str = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;';
            switch (val) {
                case 1:
                    str = str + 'background-color: #E66767;">未审批';
                    break;
                case 2:
                    str = str + 'background-color: #6BC8FB;">已审批'
                    break;
                default:
                    str = str + '">';
                    break;
            }

            return str + '</span>';
        },
        //合同状态
        ConState: function (val) {
            var str = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;';
            switch (val) {
                case 1:
                    str = str + 'background-color: #E66767;">初稿';
                    break;
                case 2:
                    str = str + 'background-color: #6BC8FB;">审批'
                    break;
                case 3:
                    str = str + 'background-color: #ACCC6B;">有效'
                    break;
                case 5:
                    str = str + 'background-color: #FFD700;">驳回'
                    break;
                case 6:
                    str = str + 'background-color: #FFD700;">无效'
                    break;
                default:
                    str = str + 'background-color: #CACACA;">失效';
                    break;
            }

            return str + '</span>';
        },
        //审批状态
        ApprovalState: function (pStatus, item) {
            var strStatus = item.Status;
            switch (item.InforType) {
                case '10008001':
                    strStatus = pStatus ? '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: green;font-weight:bold">已审批</span>' : '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: orange;font-weight:bold">未审批</span>';
                    break;
                case '10008002':
                    strStatus = pStatus ? '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: green;font-weight:bold">已完成</span>' : '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: orange;font-weight:bold">未完成</span>';
                    break;
                case '10008003':
                    strStatus = pStatus ? '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: green;font-weight:bold">已读</span>' : '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: orange;font-weight:bold">未读</span>';
                    break;
            }
            return strStatus;
        },
        // 获得数据字典对应值
        GetDiction: function (key) {
            var dictions = Ext.decode(config.idata.sysInfo.dictions);
            var dName = '';
            Ext.Array.each(dictions, function (item) {
                if (item.DictionKey == key) {
                    dName = dName + item.DictionValue
                }
            });
            return dName;
        },

        //获得数据字典对应值
        GetStyleDiction: function (key) {
            var dName = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #17AFA6;">' + this.GetDiction(key) + '</span>';
            return dName;
        },
        //更新当前用户设备信息
        UpdateEquipment: function () {
            //获得用户登陆信息
            var user = Ext.decode(util.storeGet("logininfor")),
                pushUser;

            setTimeout(function () {
                window.pushInfor("", function (value) {
                    pushUser = Ext.decode(value);
                    Ext.apply(pushUser, { UserId: user.Id });

                    //提交设备信息
                    Ext.Ajax.request({
                        url: config.url + '/PhoneUserMarked/AddOrUpdate',
                        params: pushUser,
                        success: function (response) { },
                        failure: function () { }
                    });
                });
            });
        },
        // 启动流程
        InitiateProcess: function (params, success, failure) {

            //Ext.Msg.setCls('ux_msg');
            util.showMessage("流程启动中...", false);
            Ext.Ajax.request({
                url: config.url + "/WFTemple/PlayWF",//提交地址
                params: params,
                success: function (response) {
                    util.hideMessage();
                    var result = Ext.decode(response.responseText);
                    util.showMessage(result.msg, true);

                    if (Ext.isFunction(success))
                        success(result);
                },
                failure: function (result, request) {
                    util.hideMessage();
                    util.showMessage("启动失败", true);
                    if (Ext.isFunction(failure))
                        failure(result);
                }
            });
        },
        // 设置底部菜单
        SetBottomBar: function (view) {
            var url = view.id.replace(/\_/g, '.');
            if (!this._bottomBar) {
                this._bottomBar = {
                    xtype: 'panel',
                    cls: 'bottomBar',
                    layout: 'hbox',
                    style: {
                        'border-top': '1px solid #CCC',
                    },
                    scrollDock: 'bottom',
                    docked: 'bottom',
                    items: [{
                        xtype: 'button',
                        iconCls: 'user',
                        flex: .1,
                        handler: function (but) {
                            view.fireEvent("SetPerson", url, view);
                        }
                    }, {
                        xtype: 'textareafield',
                        height: 20,
                        flex: 0.8,
                        placeHolder: '发标评论...'
                    }, {
                        xtype: 'button',
                        text: '提交',
                        flex: .2,
                        handler: function (but) {
                            //view.fireEvent("SubComment", but.up("panel").down("textareafield").getValue(), but.up("panel"), view);
                            var content = but.up("panel").down("textareafield");

                            Ext.Ajax.request({
                                url: config.url + '/Comment/Add',
                                params: {
                                    Title: '信息评论',
                                    Content: content.getValue(),
                                    AId: view.data.Id,
                                    ShareId: view.PersonIds.toString(),
                                    PageUrl: url
                                },
                                success: function (response) {
                                    content.setValue("");
                                }
                            });
                        }
                    }],
                    subComment: function (id, content) {
                        alert(id);
                    }
                }
            }
            return this._bottomBar;
        },
        // 得到项目结构
        ProjectName: function (value) {
            var projectname = [];
            var html = "";
            var nbsp = '';
            if (value) {
                var Arrys = value.split("-");
                for (var i = Arrys.length - 1; i >= 0; i--) {
                    nbsp = nbsp + '&nbsp;';
                    if (html) {
                        html = html + '<h1>' + nbsp + Arrys[i] + '</h1>'
                    } else {
                        html = '<h1>&nbsp;' + Arrys[i] + '</h1> '
                    }
                }
            }
            return html;
        },
        // 刷新菜单
        RefreshMenu: function () {
            var view, id = 'SH.Main.Menu'.replace(/\./g, '_');
            view = Ext.getCmp(id);
            if (view) {
                view.loadMenu();
            };
        },
        // 加载页面菜单
        loadMenu: function (cName, successFn) {
            var menus = [];
            var mItems = [];

            var data = [{
                height: 150,
                layout: 'fit',
                items: [{
                    xtype: 'topImg',
                }]
            }, {
                flex: 6,
                cls: 'home',
                scrollable: {
                    directionLock: true
                },
                style: {
                    'margin-top': '10px'
                },
                defaults: {
                    height: '7em',
                    layout: 'hbox',
                    defaults: {
                        flex: 1
                    }
                },
                xtype: 'panel',
                listeners: {
                    initialize: function (view, e) {
                        util.showMessage('Loading...', false);
                        Ext.Ajax.request({
                            wait: '111',
                            url: config.url + '/MenuUser/GetByPhone',
                            params: { CName: cName, projectId: config.project.pId },
                            async: true,
                            success: function (response) {
                                util.hideMessage();
                                var rdata = Ext.decode(response.responseText);
                                if (rdata.success) {
                                    var dMenus = rdata.data;
                                    dMenus.forEach(function (item, index) {
                                        var ys = (index + 1) % 3;
                                        mItems.push({
                                            xtype: 'button',
                                            iconAlign: 'top',
                                            //iconCls: 'rzgl',
                                            iconCls: item.IconClass + ' wz',
                                            text: item.MenuName,
                                            tobj: item,
                                            handler: function (but) {
                                                successFn(but);
                                            }
                                        });
                                        if (ys == 0) {
                                            menus.push({ items: mItems });
                                            mItems = [];
                                        } else {
                                            if (index + 1 == dMenus.length) {
                                                menus.push({ width: (33.33 * ys) + '%', items: mItems });
                                            }
                                        }
                                    });
                                    view.setItems(menus);
                                }
                            },
                            failure: function () {
                                util.hideMessage();
                            }
                        });
                    }
                }
            }];

            return data;
        },
        // 登录过期,重新登录
        ReLogin: function (msg) {
            //Ext.Msg.setCls('ux_msg');
            alert(msg)
            util.redirectTo('SH.Main.Login', '', { parentUrl: 'SH.Main.Layout' })
        },
        // 加载页面配置数据
        LoadTempletData: function (id, tData) {

            if (config.idata && config.idata.myInfo) {
                // 默认列表配置数据
                var data = {
                    title: '{Id}',
                    content: [
                        { label: '创建时间', name: 'CreatedTime', value: '2016-01-28 12:00:00', analytic: 'SHUtil.FormatTime' },
                        { label: '修改时间', name: 'LastUpdatedTime', value: '2016-01-28 12:00:00', analytic: 'SHUtil.FormatTime' }
                    ]
                };
                // 视图中定义的列表数据
                if (tData) {
                    data = tData;
                }

                // 获取List配置信息
                var listConfig = util.storeGet(config.idata.myInfo.userId + id);

                // 如果list配置信息为空,则加载服务器list配置
                if (listConfig) {
                    var objConfig = Ext.decode(listConfig);
                    data = Ext.decode(objConfig.Parameter);
                } else {
                    Ext.Array.each(config.idata.myInfo.listConfigs, function (item) {
                        if (item.ViewId == id) {
                            data = Ext.decode(item.Parameter);
                        }
                    });
                }
            }
            return data;
        },
        // 加载图标
        loadIcon: function (successFn) {
            var menus = [];
            var mItems = [];
            var dMenus = [
                { icon: 'qywh' }, { icon: 'ztbxi' }, { icon: 'gczs' }, { icon: 'xwzx' }, { icon: 'rlzy' }, { icon: 'tqyb' }, { icon: 'htgl' }, { icon: 'srhtgl' }, { icon: 'zchtgl' }, { icon: 'clgl' },
                { icon: 'rksq' }, { icon: 'clsq' }, { icon: 'cksq' }, { icon: 'clpd' }, { icon: 'rsgl' }, { icon: 'rygl' }, { icon: 'qjsq' }, { icon: 'tzgl' }, { icon: 'qdtzgl' }, { icon: 'aqtzgl' },
                { icon: 'cstzgl' }, { icon: 'zdftzgl' }, { icon: 'cggl' }, { icon: 'cscgjh' }, { icon: 'cscgyl' }, { icon: 'gzl' }, { icon: 'wdlc' }, { icon: 'wdrw' }, { icon: 'xmyjd' }, { icon: 'xmyjh' },
                { icon: 'rzgl' }, { icon: 'qdrz' }, { icon: 'xhrz' }, { icon: 'glrz' }, { icon: 'cwzx' }, { icon: 'bxsq' }, { icon: 'jksq' }, { icon: 'jlgl' }, { icon: 'zlgl' }, { icon: 'gzrw' },
                { icon: 'aqgl' }, { icon: 'sygl' }, { icon: 'jsgl' }, { icon: 'xmjd' }, { icon: 'xmjh' }];
            dMenus.forEach(function (item, index) {
                var ys = (index + 1) % 3;
                mItems.push({
                    xtype: 'button',
                    iconAlign: 'top',
                    iconCls: item.icon,

                    tobj: item,
                    handler: function (but) {
                        successFn(but);
                    }
                });
                if (ys == 0) {
                    menus.push({ items: mItems });
                    mItems = [];
                } else {
                    if (index + 1 == dMenus.length) {
                        menus.push({ width: (33.33 * ys) + '%', items: mItems });
                    }
                }
            });

            var data = [{
                scrollable: {
                    directionLock: true
                },

                defaults: {
                    height: '6em',
                    layout: 'hbox',
                    defaults: {
                        flex: 1
                    }
                },
                xtype: 'panel',
                cls: 'home',
                items: menus
            }];
            return data;
        },
        // 设置底部安装
        SetBottomBarAn: function (view) {
            var url = view.id.replace(/\_/g, '.');
            if (!view._bottomBar) {
                view._bottomBar = {
                    xtype: 'panel',
                    cls: 'bottomBar',
                    layout: 'hbox',
                    style: {
                        'border-top': '1px solid #CCC',
                    },
                    scrollDock: 'bottom',
                    docked: 'bottom',
                    items: [{
                        xtype: 'button',
                        text: '安装',
                        height: 40,
                        flex: .2,
                        handler: function (but) {
                            Ext.apply(view.data, {
                                ParentName: view.cName,
                                ProjectId: config.project.pId,
                                UserId: config.idata.myInfo.userId
                            });

                            Ext.Ajax.request({
                                url: config.url + '/MenuUser/Add',
                                params: view.data,
                                success: function (response) {
                                    util.redirectTo(view.rootUrl, "back", { rType: 'Install' });
                                }
                            });
                        }
                    }],
                    subComment: function (id, content) {
                    }
                }
            }
            return view._bottomBar;
        },
        Update: function (value) {
            rStr = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + value + '</span>';
            return rStr;
        },
        //单位
        SelectUnit: function (ViewSuccess) {
            var me = this;
            if (!me._UnitPanel) {
                me._UnitPanel = Ext.create('Ext.Panel', {
                    modal: true,
                    id: 'select_unit_panel',
                    width: '90%',
                    zIndex: '80000',
                    height: '80%',
                    centered: true,
                    hideOnMaskTap: true,
                    //搜索方法
                    listeners: {
                        Search: function () {
                            var dicData = [];
                            var tplTitle = this.down('textfield[name="tplTitle"]').getValue();
                            Ext.Array.each(config.dics, function (item) {
                                var isFind = item.Tobject.DictionName.indexOf(tplTitle) == -1 ? false : true;
                                if (isFind) {
                                    dicData.push({
                                        Text: item.Text,
                                        Value: item.Value,
                                        Tobject: item.Tobject,
                                    });
                                }
                            });
                            me._UnitPanel.down('list').getStore().setData(dicData);
                        }
                    },
                    items: [
                        {
                            docked: 'top',
                            xtype: 'navigationbar',
                            cls: 'vEdit',
                            html: '单位列表'
                        },
                        {
                            xtype: 'panel',
                            layout: 'hbox',
                            height: '10%',
                            defaults: {
                                style: 'padding:0em;margin:.5em;',
                            },
                            items: [
                                {
                                    flex: 9,
                                    xtype: 'textfield',
                                    cls: 'vEditFont',
                                    name: 'tplTitle',
                                    id: '',
                                    placeHolder: '输入关键字'
                                },
                                {
                                    flex: 1,
                                    xtype: 'button',
                                    cls: 'vEditButton',
                                    text: '搜索',
                                    handler: function (but) {
                                        this.up('[id="select_unit_panel"]').fireEvent('Search', but);
                                    }
                                }
                            ]
                        },
                        {
                            xtype: 'panel',
                            height: '90%',
                            layout: 'fit',
                            items: [
                                {
                                    xtype: 'list',
                                    itemTpl: '{Text}',
                                    store: {
                                        fields: ['Text', 'Value'],
                                        data: config.dics
                                    },
                                    // 这里是监听点击列表某一项后所执行的方法
                                    listeners: {
                                        itemtap: function (view, index, target, record, e, eOpts) {
                                            view.config.vSucesss({ Unit: record.data.Value, Units: record.data.Text });
                                            me._UnitPanel.hide();
                                        }
                                    },
                                    vSucesss: ViewSuccess
                                }
                            ]
                        }
                    ]
                });
                Ext.Viewport.add(me._UnitPanel);
            } else {
                me._UnitPanel.down('list').config.vSucesss = ViewSuccess;
            }
            me._UnitPanel.show();
        },
        //是否激活
        IsVisible: function (val) {
            var str = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;';
            switch (val) {
                case false:
                    str = str + 'background-color: #E66767;">未激活';
                    break;
                case true:
                    str = str + 'background-color:rgba(245, 178, 102, 0.72)">激活'
                    break;
            }

            return str + '</span>';
        },
        //是否归还
        IsReturn: function (val) {
            var str = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;';
            switch (val) {
                case false:
                    str = str + 'background-color: #E66767;">否';
                    break;
                case true:
                    str = str + 'background-color:#E66767;">是'
                    break;
            }

            return str + '</span>';
        },
        //图标样式
        IconStyle: function (val) {
            rStr = "<div style='width:3em;height:3em; background-color: #eee !important; 'class='" + val + " wz '>&nbsp;&nbsp;&nbsp;</div>";
            return rStr;
        },
        //流程详细按钮
        WFPro: function (value) {
            var rStr =
                '<div style="float:left;width:50%">' +
                    this.WFState(value) +
                '</div>' +
                '<div style="float:left;width:50%;text-align:right;">' +
                    '<span class="WFProess" style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: #ACCC6B;font-weight:bold">流程详细</span>' +
                '</div>';
            return rStr;
        },
        //借款信息
        OpenBorrow: function (value, text) {
            var rStr =
                '<div style="float:left;width:50%">' +
                    value + '元' +
                '</div>' +
                '<div style="float:left;width:50%;text-align:right;">' +
                    '<span class="Borrow" style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: #ACCC6B;font-weight:bold">' + text + '</span>' +
                '</div>';
            return rStr;
        },
        //流程配置人员
        WFConfig: function (view, record, applyId) {
            var me = this;
            me.field = 'title';
            // 保存数据
            Ext.Ajax.request({
                //url: config.url + '/User/GetsByProjectIdFilter',//提交地址GetAll
                url: config.url + '/User/GetsByProjectIdFilter',
                params: { ProjectId: config.project.pId },
                success: function (response) {
                    var result = Ext.decode(response.responseText);
                    if (result.success) {
                        data = result.data;
                        me.SelectUser(setValues, data);
                        var tplTitle = me._UserPanels.down('textfield[name="tplTitle"]').setValue("");
                        function setValues(user) {
                            me.addUpadteWFConfig(view, record.data, user, applyId)
                        }
                    } else {
                        util.showMessage(result.msg, true);
                    }
                }
            });
        },
        //配置人员
        SelectUser: function (ViewSuccess, data) {
            var me = this;
            if (!me._UserPanels) {
                me._UserPanels = Ext.create('Ext.Panel', {
                    modal: true,
                    id: 'select_user_panels',
                    width: '90%',
                    height: '80%',
                    centered: true,
                    hideOnMaskTap: true,
                    //搜索方法
                    listeners: {
                        Search: function () {
                            var userData = [];
                            var tplTitle = this.down('textfield[name="tplTitle"]').getValue();
                            Ext.Array.each(data, function (item) {
                                var isFind = item.LoginName.toLowerCase().indexOf(tplTitle.toLowerCase()) == -1 ? false : true;
                                if (isFind) {
                                    userData.push({
                                        Name: item.Name,
                                        LoginName: item.LoginName,
                                        Id: item.Id,
                                    });
                                }
                            });

                            me._UserPanels.down('list').getStore().setData(userData);
                        }
                    },
                    items: [
                        {
                            docked: 'top',
                            xtype: 'navigationbar',
                            cls: 'vEdit',
                            html: '人员列表'
                        },
                        {
                            xtype: 'panel',
                            layout: 'hbox',
                            height: '10%',
                            defaults: {
                                style: 'padding:0em;margin:.5em;',
                            },
                            items: [
                                {
                                    flex: 9,
                                    xtype: 'textfield',
                                    cls: 'vEditFont',
                                    name: 'tplTitle',
                                    id: '',
                                    placeHolder: '输入关键字'
                                },
                                {
                                    flex: 1,
                                    xtype: 'button',
                                    cls: 'vEditButton',
                                    text: '搜索',
                                    handler: function (but) {
                                        this.up('[id="select_user_panels"]').fireEvent('Search', but);
                                    }
                                }
                            ]
                        },
                        {
                            xtype: 'panel',
                            height: '90%',
                            layout: 'fit',

                            items: [
                                {
                                    xtype: 'list',
                                    itemTpl: '{Name}',

                                    store: {
                                        fields: ['Name', 'Id', 'LoginName'],
                                        data: data,
                                    },
                                    // 这里是监听点击列表某一项后所执行的方法
                                    listeners: {
                                        itemtap: function (view, index, target, record, e, eOpts) {
                                            view.config.vSucesss({ UserName: record.data.Name, UserId: record.data.Id });
                                            me._UserPanels.hide();
                                        }
                                    },
                                    vSucesss: ViewSuccess
                                }
                            ]
                        }
                    ]
                });
                Ext.Viewport.add(me._UserPanels);
            } else {
                me._UserPanels.down('list').config.vSucesss = ViewSuccess;
                me._UserPanels.down('list').getStore().setData(data);
            }
            me._UserPanels.show();
        },
        // 流程配置设置
        addUpadteWFConfig: function (view, recond, user, applyId) {
            var me = this;
            var Url = ""
            if (recond.Id == "00000000-0000-0000-0000-000000000000" || recond.Id == "") {
                var data = Ext.apply(recond, {
                    Id: "",
                    ApplyId: applyId,
                    Value: user.UserId,
                    MainId: recond.MainId,
                    CreateUserID: Ext.JSON.decode(util.storeGet("logininfor")).Id

                });
                Url = config.url + '/WFConfig/Add'

            } else {
                var data = Ext.apply(recond, {
                    Value: user.UserId
                });
                Url = config.url + '/WFConfig/Update'
            }
            Ext.Ajax.request({
                url: Url,
                async: false,
                params: data,
                success: function (response) {
                    var result = Ext.decode(response.responseText);
                    if (result.success) {
                        view.getStore().load();
                        util.showMessage("配置成功", true);
                    }

                }
            })
        },
        //是否流程配置
        IsCofing: function (ApplyId, parentUrl, runWF, controllerName) {
            Ext.Msg.setCls('ux_msg');
            util.redirectTo("SH.App.BPM.WFConfig.List", "", { parentUrl: parentUrl, ApplyId: ApplyId, RunWF: runWF, cName: controllerName });
            //Ext.Msg.confirm('操作确认', '是否需要配置流程审批者?', function (btn) {
            //    if (btn == 'yes') {
            //        util.redirectTo("SH.App.BPM.WFConfig.List", "", { parentUrl: parentUrl, ApplyId: ApplyId, RunWF: runWF, cName: controllerName });
            //    } else {
            //        runWF();
            //    }
            //}, this);
        },
        //选择人员
        SelectUsers: function (ViewSuccess, data) {
            var me = this;
            if (!me._UserPaneles) {
                me._UserPaneles = Ext.create('Ext.Panel', {
                    modal: true,
                    id: 'select_user_SelectUsers',
                    width: '90%',
                    height: '80%',
                    centered: true,
                    hideOnMaskTap: true,
                    //搜索方法
                    listeners: {
                        Search: function () {
                            var userData = [];
                            var tplTitle = this.down('textfield[name="tplTitle"]').getValue();
                            Ext.Array.each(data, function (item) {
                                var isFind = item.Name.indexOf(tplTitle) == -1 ? false : true;
                                if (isFind) {
                                    userData.push({
                                        Name: item.Name,
                                        Post: item.Post,
                                        Tobject: item.Tobject,
                                        BasicWage: item.BasicWage,
                                        Housing: item.Housing,
                                        PostSalary: item.PostSalary,
                                        MealAllowance: item.MealAllowance,
                                        FullAttendance: item.FullAttendance,
                                        CarAllowance: item.CarAllowance,
                                        ShouldBeSent: item.ShouldBeSent,
                                    });
                                }
                            });
                            me._UserPaneles.down('list').getStore().setData(userData);
                        }
                    },
                    items: [
                        {
                            docked: 'top',
                            xtype: 'navigationbar',
                            cls: 'vEdit',
                            html: '人员列表'
                        },
                        {
                            xtype: 'panel',
                            layout: 'hbox',
                            defaults: {
                                style: 'padding:0em;margin:.5em;',
                            },
                            items: [
                                {
                                    flex: 9,
                                    xtype: 'textfield',
                                    cls: 'vEditFont',
                                    name: 'tplTitle',
                                    id: '',
                                    placeHolder: '输入关键字'
                                },
                                {
                                    flex: 1,
                                    xtype: 'button',
                                    cls: 'vEditButton',
                                    text: '搜索',
                                    handler: function (but) {
                                        this.up('[id="select_user_SelectUsers"]').fireEvent('Search', but);
                                    }
                                }
                            ]
                        },
                        {
                            xtype: 'panel',
                            height: '100%',
                            layout: 'fit',
                            items: [
                                {
                                    xtype: 'list',
                                    itemTpl: '{Name}',
                                    store: {
                                        fields: ['Name', 'Id', 'LoginName', 'BasicWage', 'ShouldBeSent', 'Housing', 'PostSalary', 'MealAllowance', 'FullAttendance', 'CarAllowance'],
                                        data: data
                                    },
                                    // 这里是监听点击列表某一项后所执行的方法
                                    listeners: {
                                        itemtap: function (view, index, target, record, e, eOpts) {
                                            view.config.vSucesss({
                                                UserName: record.data.Name,
                                                UserId: record.data.Id,
                                                BasicWage: record.data.BasicWage,
                                                Housing: record.data.Housing,
                                                PostSalary: record.data.PostSalary,
                                                MealAllowance: record.data.MealAllowance,
                                                FullAttendance: record.data.FullAttendance,
                                                CarAllowance: record.data.CarAllowance,
                                                ShouldBeSent: record.data.ShouldBeSent,
                                            });
                                            me._UserPaneles.hide();
                                        }
                                    },
                                    vSucesss: ViewSuccess
                                }
                            ]
                        }
                    ]
                });
                Ext.Viewport.add(me._UserPaneles);
            } else {
                me._UserPaneles.down('list').getStore().setData(data);
                me._UserPaneles.down('list').config.vSucesss = ViewSuccess;
            }
            me._UserPaneles.show();
        },
        // 获取处理状态
        ReimbursementStatus: function (val) {
            var str = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;';
            switch (val) {
                case 0:
                    str = str + 'background-color: #E66767;">未报销';
                    break;
                case 1:
                    str = str + 'background-color: #6BC8FB;">报销中'
                    break;
                case 2:
                    str = str + 'background-color: #ACCC6B;">已报销'
                    break;
            }

            return str + '</span>';
        },
        //赋值选择人员 赋值
        SetUserValue: function (ViewSuccess) {
            var me = this;
            me.field = 'title';
            // 保存数据
            Ext.Ajax.request({
                url: config.url + '/User/GetsByProjectId',
                params: { ProjectId: config.project.pId },
                success: function (response) {
                    var result = Ext.decode(response.responseText);
                    if (result.success) {
                        data = result.data;
                        me.SelectProejctUsers(ViewSuccess, data);
                        var tplTitle = me._UserPanel.down('textfield[name="tplTitle"]').setValue("");
                        function setValues(data) {
                            view.setValues(data);
                        }
                    } else {
                        util.showMessage(result.msg, true);
                    }
                }
            });
        },
        //选择项目下人员
        SelectProejctUsers: function (ViewSuccess, data) {
            var me = this;
            if (!me._UserPanel) {
                me._UserPanel = Ext.create('Ext.Panel', {
                    modal: true,
                    id: 'select_user_panel',
                    width: '90%',
                    height: '80%',
                    zIndex: '80000',
                    centered: true,
                    hideOnMaskTap: true,
                    //搜索方法
                    listeners: {
                        Search: function () {
                            var userData = [];
                            var tplTitle = this.down('textfield[name="tplTitle"]').getValue();
                            Ext.Array.each(data, function (item) {
                                var isFind = item.Name.indexOf(tplTitle) == -1 ? false : true;
                                if (isFind) {
                                    userData.push({
                                        Name: item.Name,
                                        LoginName: item.LoginName,
                                        Id: item.Id,
                                    });
                                }
                            });
                            me._UserPanel.down('list').getStore().setData(userData);
                        }
                    },
                    items: [
                        {
                            docked: 'top',
                            xtype: 'navigationbar',
                            cls: 'vEdit',
                            html: '人员列表'
                        },
                        {
                            xtype: 'panel',
                            layout: 'hbox',
                            defaults: {
                                style: 'padding:0em;margin:.5em;',
                            },
                            items: [
                                {
                                    flex: 9,
                                    xtype: 'textfield',
                                    cls: 'vEditFont',
                                    name: 'tplTitle',
                                    id: '',
                                    placeHolder: '输入关键字'
                                },
                                {
                                    flex: 1,
                                    xtype: 'button',
                                    cls: 'vEditButton',
                                    text: '搜索',
                                    handler: function (but) {
                                        this.up('[id="select_user_panel"]').fireEvent('Search', but);
                                    }
                                }
                            ]
                        },
                        {
                            xtype: 'panel',
                            height: '100%',
                            layout: 'fit',
                            items: [
                                {
                                    xtype: 'list',
                                    itemTpl: '{Name}',
                                    store: {
                                        fields: ['Name', 'Id', 'LoginName', 'BasicWage', 'ShouldBeSent', 'Housing', 'PostSalary', 'MealAllowance', 'FullAttendance', 'CarAllowance'],
                                        data: data
                                    },
                                    // 这里是监听点击列表某一项后所执行的方法
                                    listeners: {
                                        itemtap: function (view, index, target, record, e, eOpts) {
                                            view.config.vSucesss({ Name: record.data.Name, Id: record.data.Id });
                                            me._UserPanel.hide();
                                        }
                                    },
                                    vSucesss: ViewSuccess
                                }
                            ]
                        }
                    ]
                });
                Ext.Viewport.add(me._UserPanel);
            } else {
                me._UserPanel.down('list').getStore().setData(data);
                me._UserPanel.down('list').config.vSucesss = ViewSuccess;
            }
            me._UserPanel.show();
        },

        //选择角色
        SelectRoles: function (ViewSuccess, data) {
            var me = this;
            if (!me._RolesPanel) {
                me._RolesPanel = Ext.create('Ext.Panel', {
                    modal: true,
                    id: 'select_RolesPanel',
                    width: '90%',
                    height: '80%',
                    zIndex: '80000',
                    centered: true,
                    hideOnMaskTap: true,
                    //搜索方法
                    listeners: {
                        Search: function () {
                            var userData = [];
                            var tplTitle = this.down('textfield[name="tplTitle"]').getValue();
                            Ext.Array.each(data, function (item) {
                                var isFind = item.Name.indexOf(tplTitle) == -1 ? false : true;
                                if (isFind) {
                                    userData.push({
                                        Name: item.Name,
                                        LoginName: item.LoginName,
                                        Id: item.Id,
                                    });
                                }
                            });
                            me._RolesPanel.down('list').getStore().setData(userData);
                        }
                    },
                    items: [
                        {
                            docked: 'top',
                            xtype: 'navigationbar',
                            cls: 'vEdit',
                            html: '角色列表'
                        },
                        {
                            xtype: 'panel',
                            layout: 'hbox',
                            defaults: {
                                style: 'padding:0em;margin:.5em;',
                            },
                            items: [
                                {
                                    flex: 9,
                                    xtype: 'textfield',
                                    cls: 'vEditFont',
                                    name: 'tplTitle',
                                    id: '',
                                    placeHolder: '输入关键字'
                                },
                                {
                                    flex: 1,
                                    xtype: 'button',
                                    cls: 'vEditButton',
                                    text: '搜索',
                                    handler: function (but) {
                                        this.up('[id="select_RolesPanel"]').fireEvent('Search', but);
                                    }
                                }
                            ]
                        },
                        {
                            xtype: 'panel',
                            height: '100%',
                            layout: 'fit',
                            items: [
                                {
                                    xtype: 'list',
                                    itemTpl: '{Name}',
                                    store: {
                                        fields: ['Name', 'Id', 'Code', 'SortOrder'],
                                        data: data
                                    },
                                    // 这里是监听点击列表某一项后所执行的方法
                                    listeners: {
                                        //单击
                                        itemtap: function (view, index, target, record, e, eOpts) {
                                            view.config.vSucesss({ Name: record.data.Name, Id: record.data.Id });
                                            me._RolesPanel.hide();
                                        }
                                    },
                                    vSucesss: ViewSuccess
                                }
                            ]
                        }
                    ]
                });
                Ext.Viewport.add(me._RolesPanel);
            } else {
                me._RolesPanel.down('list').getStore().setData(data);
                me._RolesPanel.down('list').config.vSucesss = ViewSuccess;
            }
            me._RolesPanel.show();
        },
        //获取收入计划
        DicGetIP: function (value) {
            var IP = diction.byData(value, config.url + "/IncomePlan/ComboxsShow");
            var con = IP.data ? IP.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取支出计划
        DicGetDPText: function (value) {
            var data = diction.byData(value, config.url + '/DefrayPlan/GetCombox?projectId=' + config.project.pId);
            var text = '';
            if (data && data.length > 0) {
                text = data[0].Text;
            }
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + text + '</span>';
            return con;
        },
        //获取部位名称
        DicGetPIText: function (value) {
            var text = '';
            //提交设备信息
            Ext.Ajax.request({
                url: config.url + '/IncomeList/GetByPIId',
                params: { PIId: value },
                success: function (response) {

                },
                failure: function () { }
            });
            var con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + text + '</span>';
            return con;
        },
        //获取支出清单
        DicGetDefrayList: function (value) {
            var DefrayList = diction.byData(value, config.url + "/DefrayList/GetByKey");
            var con = DefrayList.data ? DefrayList.data.Name : "";
            //con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取支出清单
        DicGetDefrayListStyle: function (value) {
            var DefrayList = diction.byData(value, config.url + "/DefrayList/GetByKey");
            var con = DefrayList.data ? DefrayList.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },

        //获取结算
        DicGetBillingStyle: function (value) {
            var Billing = diction.byData(value, config.url + "/Billing/GetByKey");
            var con = Billing.data ? Billing.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //百分比显示
        SetProgress: function (value) {
            var valS = (value) * 100 + "%"
            var val = "100%"
            if (value < 1) {
                val = value * 100 + "%"
            }
            var html = '<div class="progress-bar"><div class="progress-b" style="width:' + val + '"><span>' + valS + '</span></div></div> '
            //return value.toFixed(2) * 100 + "%";
            return html;
        },
        //登录用户是否有团队
        Team: function () {
            Ext.Ajax.request({
                url: config.url + '/Project/GetByUser',
                success: function (response) {
                    var result = Ext.decode(response.responseText);
                    var length = result.length;
                    return length;
                }
            });
        },
        // 显示图片
        ShowImg: function (imgIds) {
            //imgIds = 'D9AC074E-F414-4448-AEB9-A61D010CD5A6,14618E0B-BE7C-41A1-81A1-A61D010AAD2A'
            var imgHtml = '<div style="width：100%">';
            //加载用户信息
            Ext.Ajax.request({
                url: config.url + '/Accessory/GetByIds',
                params: { ids: imgIds },
                async: false,
                success: function (response) {
                    var rdata = Ext.decode(response.responseText);
                    if (rdata.success) {
                        if (rdata.data) {
                            Ext.Array.each(rdata.data, function (item) {
                                var webPath = config.url + item.Path;
                                var path = util.FileShow(item.Path, item.FType);
                                imgHtml += '<img src="' + path + '" width="32%" path="' + item.Path + '" title="' + item.Title + '"  id="' + item.Id + '" style="margin-right: .3em;" class="open_file_Info"/>';
                            });
                        }
                    }
                }
            });
            return imgHtml + "</div>";
        },
        // 打开清单
        OpenList: function (text, className) {
            var html = '<span class="' + className + '" style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white ;background-color: #ACCC6B;font-weight:bold">' + text + '</span>';
            return html;
        },
        // 打开通知信息
        OpenNotice: function () {
            util.redirectTo('SH.Main.Layout', '', {})
            var main_layout = Ext.getCmp('SH_Main_Layout');
            main_layout.setActiveItem(2);
        },
        // 是否登录
        IsLogin: function () {
            if (!config.idata || !config.idata.myInfo) {
                return false;
            } else {
                return true;
            }
        },
        //获取使用人
        FillPeople: function (value) {
            var FillPeoples = "";
            var FillPeopleIds = value.split(",");
            for (var i = 0; i < FillPeopleIds.length; i++) {
                if (FillPeoples == "") {
                    FillPeoples = this.DicGetUser(FillPeopleIds[i]);
                } else {
                    FillPeoples = FillPeoples + "," + this.DicGetUser(FillPeopleIds[i])
                }
            }
            return FillPeoples;
        },
        //选择项目合同（选择结算,计划）
        ContractList: function (ViewSuccess, data, title) {
            var me = this;
            if (!me._ContractPanel) {
                me._ContractPanel = Ext.create('Ext.Panel', {
                    modal: true,
                    id: 'select_user_ContractList',
                    width: '90%',
                    height: '80%',
                    centered: true,
                    hideOnMaskTap: true,
                    //搜索方法
                    listeners: {
                        Search: function () {
                            var userData = [];
                            var tplTitle = this.down('textfield[name="tplTitle"]').getValue();
                            Ext.Array.each(data, function (item) {
                                var isFind = item.Name.indexOf(tplTitle) == -1 ? false : true;
                                if (isFind) {
                                    userData.push({
                                        Name: item.Name,
                                        LoginName: item.LoginName,
                                        Id: item.Id,
                                    });
                                }
                            });
                            me._ContractPanel.down('list').getStore().setData(userData);
                        }
                    },
                    items: [
                        {
                            docked: 'top',
                            xtype: 'navigationbar',
                            cls: 'vEdit',
                            html: title
                        },
                        {
                            xtype: 'panel',
                            layout: 'hbox',
                            defaults: {
                                style: 'padding:0em;margin:.5em;',
                            },
                            items: [
                                {
                                    flex: 9,
                                    xtype: 'textfield',
                                    cls: 'vEditFont',
                                    name: 'tplTitle',
                                    id: '',
                                    placeHolder: '输入关键字'
                                },
                                {
                                    flex: 1,
                                    xtype: 'button',
                                    cls: 'vEditButton',
                                    text: '搜索',
                                    handler: function (but) {
                                        this.up('[id="select_user_ContractList"]').fireEvent('Search', but);
                                    }
                                }
                            ]
                        },
                        {
                            xtype: 'panel',
                            height: '100%',
                            layout: 'fit',
                            items: [
                                {
                                    xtype: 'list',
                                    itemTpl: '{Name}',
                                    store: {
                                        fields: ['Number', 'Id', 'Name', 'ContractId', 'ContractName', 'Sum', 'Moneys'],
                                        data: data
                                    },
                                    // 这里是监听点击列表某一项后所执行的方法
                                    listeners: {
                                        itemtap: function (view, index, target, record, e, eOpts) {
                                            view.config.vSucesss({
                                                ContractId: record.data.Id, ContractName: record.data.Name,
                                                //结算
                                                BillingMoney: record.data.Sum, BillingId: record.data.Id, BillingName: record.data.Name, Moneys: record.data.Moneys,
                                                CId: record.data.Id, CIdName: record.data.Name,
                                                SPId: record.data.Id, PlanName: record.data.Name,
                                                //计划
                                                PId: record.data.Id, ParentName: record.data.Name,
                                            });
                                            me._ContractPanel.hide();
                                        }
                                    },
                                    vSucesss: ViewSuccess
                                }
                            ]
                        }
                    ]
                });
                Ext.Viewport.add(me._ContractPanel);
            } else {
                me._ContractPanel.down('list').getStore().setData(data);
                me._ContractPanel.down('list').config.vSucesss = ViewSuccess;
            }
            me._ContractPanel.show();
        },
        //获取支出清单
        DicGetPartsInventoryStyle: function (value) {
            var PList = diction.byData(value, config.url + "/PartsInventory/GetByKey");
            var con = PList.data ? PList.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取清单
        DicGetCInventoryStyle: function (value) {
            var CIList = diction.byData(value, config.url + "/CInventory/GetByKey");
            var con = CIList.data ? CIList.data.Name : "";
            con = '<span style="display:inline-block;padding: .4em .6em .5em;border-radius: .5em;color: white;background-color: #6BC8FB;">' + con + '</span>';
            return con;
        },
        //获取支出清单
        DicGetPartsInventory: function (value) {
            var PList = diction.byData(value, config.url + "/PartsInventory/GetByKey");
            var con = PList.data ? PList.data.Name : "";
            return con;
        },
        //获取清单
        DicGetCInventory: function (value) {
            var CIList = diction.byData(value, config.url + "/CInventory/GetByKey");
            var con = CIList.data ? CIList.data.Name : "";
            return con;
        },
        // 弹出框
        CreateWin: function (title, config, ok) {
            var win = Ext.create('Ext.Panel', {
                modal: true,
                width: '90%',
                hideOnMaskTap: true,
                centered: true,
                layout: 'vbox',
                cls: 'winContent',
                items: [{
                    docked: 'top',
                    xtype: 'navigationbar',
                    title: title,
                },
                {
                    xtype: "fieldset",
                    items: config
                }, {
                    xtype: 'container',
                    layout: 'hbox',
                    defaults: {
                        xtype: 'button',
                        flex: 1
                    },
                    items: [{
                        text: '取消',
                        handler: function (but) {
                            win.hide();
                        }
                    }, {
                        text: '确定',
                        handler: function (but) {
                            if (ok) {
                                ok(but);
                            }
                        }
                    }]
                }]
            });
            Ext.Viewport.add(win);

            return win;
        },

        /*
        *活动资讯
        */
        //设置 显示文字长度
        GetText: function (value) {
            var text = value;
            if (value.length > 20) {
                text = value.substring(0, 20) + "....";
            }
            return text;
        },
        //图片加载
        GetImg: function (imageUrl, contents) {
            var html = "";
            var number = 0;
            if (imageUrl) {
                var str = imageUrl.split(",")
                for (var i = 0; i < str.length; i++) {
                    number += 1;
                    if (i % 3 == 0 && i != 0) {
                        html = html + "</p>" + str[i]
                    } else {
                        html += str[i];
                    }
                    if (str.length == 1) {
                        html = '<span class="imgWidth">' + html + "</span>";
                    }
                    if (number == 3)
                        break;
                }
            }
            if (!html) {
                html = contents.substring(0, 50);
            }
            return html
        }
    }
})