Ext.define("BPM.WFModel.Layout", {
    extend: 'Ext.Panel',
    fullscreen: true,
    xtype: 'wfmodel_layout',
    requires: [
        'UX.WorkFlow.Designer',
    ],
    items: [{
        xtype: 'wfdesigner'
    }],
    //监听器
    listeners: {
        // 自定义控件属性事件处理
        execCommand: function (but, view, obj) {
            switch (obj.cmd) {
                case 'openFlow':
                    // 打开流程
                    view.openFlow(but, view, obj);
                    break;
                case 'addFlow':
                    // 添加流程
                    view.addFlow(but, view, obj);
                    break;
                case 'flowAttrSetting':
                    // 设置流程步骤属性
                    view.flowAttrSetting(but, view, obj);
                    break;
                case 'addStart':
                    // 流程开始动作
                    view.addStart(but, view, obj);
                    break;
                case 'addStep':
                    // 添加流程步骤
                    view.addStep();
                    break;
                case 'addJudge':
                    // 添加流程判断
                    view.addJudge();
                    break;
                case 'addEnd':
                    // 流程结束动作
                    view.addEnd(but, view, obj);
                    break;
                case 'addSubFlow':
                    // 添加子流程
                    // view.addSubFlow();
                    break;
                case 'addConn':
                    // 添加流程连线
                    view.addConn(but, view, obj);
                    break;
                case 'copyStep':
                    // 复制流程步骤
                    view.copyStep(but, view, obj);
                    break;
                case 'removeObj':
                    // 删除流程步骤
                    view.removeObj(but, view, obj);
                    break;
                case 'saveFlow':
                case 'saveAs':
                case 'install':
                case 'uninstall':
                case 'delete':
                    // 保存流程
                    view.saveFlow(but, view, obj);
                    break;
                case 'Run':
                    // 保存流程
                    view.Run(but, view, obj);
                    break;
                case 'Task':
                    // 保存流程
                    view.Task(but, view, obj);
                    break;
                default:
                    '';
                    break;
            }
        }
    },
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        me.myWF = me.down('wfdesigner').WF;
    },
    initComponent: function () {
        var me = this;
        var tbarData = [
            { xtype: 'button', iconCls: 'wficon_openFlow', name: '打开', cmd: 'openFlow' },
            { xtype: 'button', iconCls: 'wficon_addFlow', name: '新建', cmd: 'addFlow' },
            { xtype: 'button', iconCls: 'wficon_flowAttr', name: '属性', cmd: 'flowAttrSetting' },
            //{ xtype: 'button', iconCls: 'wficon_addStep', name: '开始', cmd: 'addStart' },
            { xtype: 'button', iconCls: 'wficon_addStep', name: '新步骤', cmd: 'addStep' },
            //{ xtype: 'button', iconCls: 'wficon_addStep', name: '判断', cmd: 'addJudge' },
            //{ xtype: 'button', iconCls: 'wficon_addStep', name: '结束', cmd: 'addEnd' },
            //{ xtype: 'button', iconCls: 'wficon_addSubFlow', name: '子流程', cmd: 'addSubFlow' },
            { xtype: 'button', iconCls: 'wficon_addConn', name: '连线', cmd: 'addConn' },
            //{ xtype: 'button', iconCls: 'wficon_copyStep', name: '复制', cmd: 'copyStep' },
            { xtype: 'button', iconCls: 'wficon_removeObj', name: '删除', cmd: 'removeObj' },
            { xtype: 'button', iconCls: 'wficon_saveFlow', name: '保存', cmd: 'saveFlow' },
            //{ xtype: 'button', iconCls: 'wficon_saveAs', name: '另存为', cmd: 'saveAs' },
            { xtype: 'button', iconCls: 'wficon_installFlow', name: '安装流程', cmd: 'install' },
            { xtype: 'button', iconCls: 'wficon_uninstallFlow', name: '卸载流程', cmd: 'uninstall' },
            { xtype: 'button', iconCls: 'wficon_deleteFlow', name: '删除流程', cmd: 'delete' },
            { xtype: 'button', iconCls: 'wficon_deleteFlow', name: '启动流程', cmd: 'Run' },
            { xtype: 'button', iconCls: 'wficon_deleteFlow', name: '任务处理', cmd: 'Task' }
        ];
        var tdata = [];
        Ext.Array.forEach(tbarData, function (item) {
            var td = {
                tooltip: item.name, text: item.name, iconCls: item.iconCls, xtype: item.xtype,
                handler: function (but) {
                    me.fireEvent('execCommand', but, me, item);
                }
            };
            tdata.push(td);
        })

        me.tbar = Ext.create("Ext.toolbar.Toolbar", {
            border: 1,
            style: {
                'background-color': '#FEFEFE'
            },
            width: '100%',
            items: tdata,
            listeners: {
                tap: function (but, e, eOpts) {
                    alert('Toolbar')
                }
            }
        });

        //UE.getEditor('ueditor', { wordCount: false, maximumWords: 1000000000, autoHeightEnabled: false });
        this.callParent(arguments);

        me.wf_json = {}; //设计json
        me.wf_steps = []; //步骤数组
        me.wf_texts = []; //文本数组
        me.wf_conns = []; //连线数组
        me.wf_focusObj = null; //当前焦点对象
        me.wf_option = ""; //当前操作
        me.wf_connColor = "#898a89"; //连线的常规颜色
        me.wf_noteColor = "#efeff0"; //节点填充颜色
        me.wf_nodeBorderColor = "#587aa9"; //节点边框颜色
        me.wf_designer = true; //是否是设计模式(查看流程图时不帮定双击事件）
        me.wf_width = 108; //步骤宽度
        me.wf_height = 50; //步骤高度
        me.myWFect = 8; //圆角大小
        me.tempArrPath = []; //临时连线

        me.mouseX = 0;
        me.mouseY = 0;
        me.drawArr();
    },
    Run: function (but, view, obj) {
        //var filterData = { "groups": [{ "rules": [{ "field": "Duration", "op": "lessorequal", "value": "2", "type": "Double" }], "op": "bitwiseand" }], "op": "bitwiseand" };
        //var da = Ext.encode(filterData);
        Ext.Ajax.request({
            url: 'WFRunInstance/PlayWF',
            params: {
                mainId: '365D37C8-9217-4D55-AEDB-4CD01659BDC6',
                mId: '365D37C8-9217-4D55-AEDB-4CD01659BDC4'
            },
            success: function (response, opts) {
                var obj = Ext.decode(response.responseText);
                util.msgTip(obj.msg);
                if (obj.success) {
                }
            },
            failure: function (response, opts) {
                console.log('server-side failure with status code ' + response.status);
            }
        });
    },
    Task: function (but, view, obj) {
        Ext.Ajax.request({
            url: 'WFRunInstance/WFTask',
            params: {
                mainId: '365D37C8-9217-4D55-AEDB-4CD01659BDC6',
                stepId: 'D97C1A26-2A29-4DCF-A305-2EAD5206E7CD'
            },
            success: function (response, opts) {
                var obj = Ext.decode(response.responseText);
                util.msgTip(obj.msg);
                if (obj.success) {
                }
            },
            failure: function (response, opts) {
                console.log('server-side failure with status code ' + response.status);
            }
        });
    },
    // 保存流程
    saveFlow: function (but, view, obj) {
        var data = {}, msg = '';
        switch (obj.cmd) {
            case 'saveFlow':
            case 'saveAs':
                data.Status = 1;
                msg = '保存';
                break;
            case 'install':
                data.Status = 2;
                msg = '安装流程';
                break;
            case 'uninstall':
                data.Status = 3;
                msg = '卸载流程';
                break;
            case 'delete':
                data.Status = 4;
                msg = '删除流程';
                break;
        };

        var isSubFrom = true, stepError = {};
        var toIds = [], fromIds = [];
        view.wf_json.Lines.forEach(function (line) {
            toIds.push(line.ToID);
            fromIds.push(line.FromID);
        });
        view.wf_json.Steps.forEach(function (step) {
            if (toIds.indexOf(step.Id, 0) == -1 && fromIds.indexOf(step.Id, 0) == -1) {
                isSubFrom = false;
                stepError = step;
            };
        });
        if (isSubFrom) {
            Ext.apply(view.wf_json, data);
            Ext.Ajax.request({
                url: 'WFModel/SaveFlow',
                params: {
                    wfjson: Ext.encode(view.wf_json),
                    projectId: ""
                },
                success: function (response, opts) {
                    var obj = Ext.decode(response.responseText);
                    if (obj.success) {
                        util.msgTip(msg + '成功');
                    }
                },
                failure: function (response, opts) {
                    util.msgTip(msg + '失败');
                }
            });
        } else {
            alert('步骤“' + stepError.Name + '”没有连线!');
        }
    },
    //复制当前选中步骤
    copyStep: function (but, view, obj) {
        if (view.wf_focusObj == null || !view.isStepObj(view.wf_focusObj)) {
            alert("请选择要复制的步骤");
            return;
        }
        var json = null;
        var text = "";
        var id = view.getGuid();
        if (view.wf_json && view.wf_json.Steps) {
            for (var i = 0; i < view.wf_json.Steps.length; i++) {
                if (view.wf_json.Steps[i].Id == view.wf_focusObj.id) {
                    json = view.wf_json.Steps[i];
                    json.Id = id;
                    text = json.Name;
                    json.Name = text;
                    break;
                }
            }
        }
        var position = { x: undefined, y: undefined, width: view.wf_width, height: view.wf_height }
        view.addStep(position, text, id, false);
    },
    //删除当前焦点及其附属对象
    removeObj: function (but, view, obj) {
        if (!view.wf_focusObj) {
            alert("请选择要删除的对象!");
        }
        else if (!confirm('您真的要删除选定对象吗?')) {
            return false;
        }
        if (view.isStepObj(view.wf_focusObj))//如果选中的是步骤
        {
            if (view.wf_focusObj.id) {
                view.wf_texts.forEach(function (item, index) {
                    if (item.id == "text_" + view.wf_focusObj.id) {
                        Ext.Array.remove(view.wf_texts, item);
                        var text = view.myWF.getById("text_" + view.wf_focusObj.id);
                        if (text) text.remove();
                    }
                });
            }
            var deleteConnIndex = new Array();
            view.wf_conns.forEach(function (item, index) {
                if (item.arrPath && (item.obj1.id == view.wf_focusObj.id || item.obj2.id == view.wf_focusObj.id)) {
                    view.deleteLine(item.id, item.arrPath.id);
                    deleteConnIndex.push(index);
                    item.arrPath.remove();
                }
            });

            for (var m = deleteConnIndex.length; m--;) {
                Ext.Array.remove(view.wf_conns, view.wf_conns[deleteConnIndex[m]]);
            }
            deleteConnIndex = new Array();

            view.wf_steps.forEach(function (item, index) {
                if (item.id == view.wf_focusObj.id) {
                    Ext.Array.remove(view.wf_steps, item);
                    view.deleteStep(view.wf_focusObj.id);
                }
            })
            view.wf_focusObj.remove();
        }
        else//如果选中的是线
        {
            view.wf_conns.forEach(function (item, index) {
                if (item.arrPath && item.arrPath.id == view.wf_focusObj.id) {
                    view.deleteLine(item.id, item.arrPath.id);
                    //view.wf_conns.remove(j);
                    Ext.Array.remove(view.wf_conns, item);
                }
            });
            view.wf_focusObj.remove();
        }
    },
    //从json中删除步骤
    deleteStep: function (stepid) {
        var me = this;
        var steps = me.wf_json.Steps;
        if (steps && steps.length > 0) {
            for (var i = 0; i < steps.length; i++) {
                if (steps[i].Id == stepid) {
                    me.removeArray(steps, i);
                }
            }
        }
    },
    //从json中删除线
    deleteLine: function (lineid, textid) {
        var me = this;
        var lines = me.wf_json.Lines;
        if (lines && lines.length > 0) {
            for (var i = 0; i < lines.length; i++) {
                if (lines[i].Id == lineid) {
                    me.removeArray(lines, i);
                }
            }
        }
        if (textid) {
            if (me.wf_texts && me.wf_texts.length > 0) {
                for (var i = 0; i < me.wf_texts.length; i++) {

                    if (me.wf_texts[i].id == "line_" + textid) {
                        me.wf_texts[i].remove();
                    }
                }
            }
        }
    },
    removeArray: function (array, n) {
        if (isNaN(n) || n > array.length) { return false; }
        array.splice(n, 1);
    },
    // 添加流程判断
    addJudge: function (id, text) {
        var me = this;
        var text;

        var guid = me.getGuid();
        var xy = me.getNewXY();
        var x = xy.x;
        var y = xy.y;
        var width = 80;
        var height = 50;
        text = text || '判断';
        id = id || guid;

        var rect = me.myWF.rect(x, y, width, height, 8);
        // 菱形实现
        //rect.transform("t100,100r45t-100,0")

        rect.attr({ "fill": me.wf_noteColor, "stroke": me.wf_nodeBorderColor, "stroke-width": 1.4, "cursor": "default" });
        rect.id = id;
        rect.type1 = 'judge';

        //拖动事件
        function move(dx, dy) {
            var myWF = me.myWF;

            var x = this.ox + dx;
            var y = this.oy + dy;

            //x = (x^2+x^2) ^ 0.5;
            //y = (y^2+y^2) ^ 0.5;
            if (x < 0) {
                x = 0;
            }
            else if (x > myWF.width - me.wf_width) {
                x = myWF.width - me.wf_width;
            }

            if (y < 0) {
                y = 0;
            }
            else if (y > myWF.height - me.wf_height) {
                y = myWF.height - me.wf_height;
            }
            this.attr("x", x);
            this.attr("y", y);
            if (this.id) {
                var text = myWF.getById('text_' + this.id);
                if (text) {
                    text.attr("x", x + 40);
                    text.attr("y", y + 25);
                }
            }
            for (var j = me.wf_conns.length; j--;) {
                if (me.wf_conns[j].obj1.id == this.id || me.wf_conns[j].obj2.id == this.id) {
                    for (var n = 0; n < me.wf_json.Lines.length; n++) {
                        if (me.wf_json.Lines[n].Id == me.wf_conns[j].arrPath.id) {
                            me.setLineText(me.wf_json.Lines[n].Id, me.wf_json.Lines[n].Text);
                            break;
                        }
                    }
                    myWF.drawArr(me.wf_conns[j]);
                }
            }
            myWF.safari();
        };

        //拖动节点开始时的事件
        function dragger() {
            this.ox = this.attr("x");
            this.oy = this.attr("y");
            changeStyle(this);
        }

        //改变节点样式
        function changeStyle(obj) {
            if (!obj) {
                return;
            }
            for (var i = 0; i < me.wf_steps.length; i++) {
                if (me.wf_steps[i].id == obj.id) {
                    me.wf_steps[i].attr("fill", me.wf_noteColor);
                    me.wf_steps[i].attr("stroke", "#cc0000");
                }
                else {
                    me.wf_steps[i].attr("fill", me.wf_noteColor);
                    me.wf_steps[i].attr("stroke", me.wf_nodeBorderColor);
                }
            }

            for (var i = 0; i < me.wf_conns.length; i++) {
                if (me.wf_conns[i].arrPath) {
                    me.wf_conns[i].arrPath.attr({ "stroke": me.wf_connColor, "fill": me.wf_connColor });
                }
            }
            //obj.animate({ }, 500);
        }

        //拖动结束后的事件
        function up() {
            changeStyle(this);
            //记录移动后的位置
            if (me.isStepObj(this)) {
                var bbox = this.getBBox();
                if (bbox) {
                    var steps = me.wf_json.Steps;
                    if (steps && steps.length > 0) {
                        for (var i = 0; i < steps.length; i++) {
                            if (steps[i].Id == this.id) {
                                steps[i].Position_x = bbox.x;
                                steps[i].Position_y = bbox.y;
                                break;
                            }
                        }
                    }
                }
            }
        }

        //单击事件执行相关操作
        function click() {
            var o = this;
            switch (me.wf_option) {
                case "line":
                    var obj = { Id: me.getGuid(), obj1: me.wf_focusObj, obj2: o };
                    me.connObj(obj);
                    break;
                default:
                    changeStyle(o);
                    break;
            }
            me.wf_option = "";
            me.wf_focusObj = this;
        }

        rect.drag(move, dragger, up);
        if (me.wf_designer) {
            rect.click(click);
            if ("normal" == rect.type1) {
                //rect.dblclick(stepSetting);
            }
            else if ("subflow" == rect.type1) {
                //rect.dblclick(subflowSetting);
            }
        }

        var text2 = text.length > 8 ? text.substr(0, 7) + "..." : text;
        var text1 = me.myWF.text(x + 40, y + 25, text2);
        text1.attr({ "font-size": "12px" });
        if (text.length > 8) text1.attr({ "title": text });
        text1.id = "text_" + id;
        text1.type1 = "text";
        me.wf_texts.push(text1);
    },
    //添加连线
    addConn: function (but, view, obj) {
        if (!view.wf_focusObj || !view.isStepObj(view.wf_focusObj)) {
            alert("请选择要连接的步骤!"); return false;
        }
        view.wf_option = "line";
        var wfd = view.down('wfdesigner');
        document.getElementById(wfd.getId()).onmousemove = function (ev) {
            view.mouseMove(ev, view);
        }
        document.getElementById(wfd.getId()).onmousedown = function () {
            for (var i = 0; i < view.tempArrPath.length; i++) {
                view.tempArrPath[i].arrPath.remove();
            }
            view.tempArrPath = [];
            document.getElementById(wfd.getId()).onmousemove = null;
        };
    },
    // 鼠标坐标
    mouseMove: function (ev, view) {
        ev = ev || window.event;
        var mousePos = view.mouseCoords(ev);
        view.mouseX = mousePos.x;
        view.mouseY = mousePos.y;
        var obj = { obj1: view.wf_focusObj, obj2: null };
        view.myWF.drawArr(obj);
    },
    // 获取坐标
    mouseCoords: function (ev) {
        if (ev.offsetX || ev.offsetY) {
            return { x: ev.offsetX - 2, y: ev.offsetY + 2 };
        }
        return {
            x: ev.clientX + document.body.scrollLeft - document.body.clientLeft,
            y: (ev.clientY + document.body.scrollTop - document.body.clientTop) + 2
        };
    },
    // 流程步骤属性
    flowAttrSetting: function (but, view, obj) {
        if (!view.wf_AddEdit) {
            //获得 添加窗体对象
            var object = {
                winTitle: '流程设置', winWidth: 650, win: 'BPM.WFModel.Edit', config: {
                    listeners: {
                        formSubmit: function (but, win) {
                            win.up('window').hide();
                            Ext.apply(view.wf_json, win.getValues());
                        }
                    }
                }
            }
            view.wf_AddEdit = util.createWindow(object);
        }
        var form = view.wf_AddEdit.down("form").getForm();
        form.reset();
        //给添加窗体赋值
        form.setValues(view.wf_json);
        // 打开窗体
        view.wf_AddEdit.show();
    },
    //设置步骤文本
    setStepText: function (id, txt) {
        var me = this;
        var stepText = me.myWF.getById("text_" + id);
        if (stepText != null) {
            if (txt.length > 8) {
                stepText.attr({ "title": txt });
                txt = txt.substr(0, 7) + "...";
            }
            stepText.attr({ "text": txt });
        }
    },
    // 添加流程
    addFlow: function (but, view, obj) {
        view.initwf();
        view.flowAttrSetting(but, view, obj);
    },
    // 初始化流程
    initwf: function () {
        var me = this;
        me.wf_json = { Id: me.getGuid() };
        me.wf_steps = new Array();
        me.wf_texts = new Array();
        me.wf_conns = new Array();
        me.myWF.clear();
    },
    // 打开流程
    openFlow: function (but, view, obj) {
        var me = this;
        var Openview = Ext.create('BPM.WFModel.OpenWin');
        var object = {
            winTitle: '打开窗体',
            win: Openview,
            winWidth: 900,
            winHeight: 500,
        }
        me.OpenWin = util.createWindow(object);
        me.OpenWin.show();
        var wfGrid = me.OpenWin.down("bpm_wfmodel_grid");
        wfGrid.on("itemdblclick", function (view, record, item, index, e, eOpts) {
            me.OpenWin.hide();
            var obj = record.data;
            me.initwf();
            me.wf_json = Ext.decode(obj.DesignJSON);
            var steps = me.wf_json.Steps;
            steps.forEach(function (item, index) {
                var position = { x: parseInt(item.Position_x), y: parseInt(item.Position_y), width: me.wf_width, height: me.wf_height };
                me.addStep(position, item.Name, item.Id, false, item.Type);
            })
            var lines = me.wf_json.Lines;
            lines.forEach(function (item, index) {
                me.connObj({ Id: item.Id, obj1: me.myWF.getById(item.FromID), obj2: me.myWF.getById(item.ToID) }, false, item.Text);
            })
            console.dir(obj);
        });
    },
    //添加步骤
    addStep: function (position, text, id, addToJSON, type1, bordercolor, bgcolor) {
        var me = this;

        var guid = me.getGuid();
        var xy = me.getNewXY();
        var x = position ? position.x || xy.x : xy.x;
        var y = position ? position.y || xy.y : xy.y;
        var width = position ? position.width || me.wf_width : me.wf_width;
        var height = position ? position.height || me.wf_height : me.wf_height;
        text = text || '新步骤';
        id = id || guid;
        var rect = me.myWF.rect(x, y, width, height, me.myWFect);
        rect.attr({ "fill": bgcolor || me.wf_noteColor, "stroke": bordercolor || me.wf_nodeBorderColor, "stroke-width": 1.4, "cursor": "default" });
        rect.id = id;
        rect.type1 = type1 ? type1 : "normal";

        //拖动事件
        function move(dx, dy) {
            var myWF = me.myWF;
            var x = this.ox + dx;
            var y = this.oy + dy;

            if (x < 0) {
                x = 0;
            }
            else if (x > myWF.width - me.wf_width) {
                x = myWF.width - me.wf_width;
            }

            if (y < 0) {
                y = 0;
            }
            else if (y > myWF.height - me.wf_height) {
                y = myWF.height - me.wf_height;
            }
            this.attr("x", x);
            this.attr("y", y);
            if (this.id) {
                var text = myWF.getById('text_' + this.id);
                if (text) {
                    text.attr("x", x + 52);
                    text.attr("y", y + 25);
                }
            }
            for (var j = me.wf_conns.length; j--;) {
                if (me.wf_conns[j].obj1.id == this.id || me.wf_conns[j].obj2.id == this.id) {
                    for (var n = 0; n < me.wf_json.Lines.length; n++) {
                        if (me.wf_json.Lines[n].Id == me.wf_conns[j].arrPath.id) {
                            me.setLineText(me.wf_json.Lines[n].Id, me.wf_json.Lines[n].Text);
                            break;
                        }
                    }
                    myWF.drawArr(me.wf_conns[j]);
                }
            }
            myWF.safari();
        };

        //拖动节点开始时的事件
        function dragger() {
            this.ox = this.attr("x");
            this.oy = this.attr("y");
            changeStyle(this);
        }

        //改变节点样式
        function changeStyle(obj) {
            if (!obj) {
                return;
            }
            for (var i = 0; i < me.wf_steps.length; i++) {
                if (me.wf_steps[i].id == obj.id) {
                    me.wf_steps[i].attr("fill", me.wf_noteColor);
                    me.wf_steps[i].attr("stroke", "#cc0000");
                }
                else {
                    me.wf_steps[i].attr("fill", me.wf_noteColor);
                    me.wf_steps[i].attr("stroke", me.wf_nodeBorderColor);
                }
            }

            for (var i = 0; i < me.wf_conns.length; i++) {
                if (me.wf_conns[i].arrPath) {
                    me.wf_conns[i].arrPath.attr({ "stroke": me.wf_connColor, "fill": me.wf_connColor });
                }
            }
            //obj.animate({ }, 500);
        }

        //拖动结束后的事件
        function up() {
            changeStyle(this);
            //记录移动后的位置
            if (me.isStepObj(this)) {
                var bbox = this.getBBox();
                if (bbox) {
                    var steps = me.wf_json.Steps;
                    if (steps && steps.length > 0) {
                        for (var i = 0; i < steps.length; i++) {
                            if (steps[i].Id == this.id) {
                                steps[i].Position_x = bbox.x;
                                steps[i].Position_y = bbox.y;
                                break;
                            }
                        }
                    }
                }
            }
        }

        //单击事件执行相关操作
        function click() {
            var o = this;
            switch (me.wf_option) {
                case "line":
                    var obj = { Id: me.getGuid(), obj1: me.wf_focusObj, obj2: o };
                    me.connObj(obj);
                    break;
                default:
                    changeStyle(o);
                    break;
            }
            me.wf_option = "";
            me.wf_focusObj = this;
        }

        //步骤属性设置
        function stepSetting() {
            var bbox = this.getBBox();
            if (!me.wf_SetStepEdit) {
                //获得 添加窗体对象
                var object = { winTitle: '步骤设置', winWidth: 650, win: 'BPM.WFModel.Step.Layout', config: { wfData: me.wf_json, wfForm: me, stepId: this.id } };
                me.wf_SetStepEdit = util.createWindow(object);
            } else {
                var data = { wfData: me.wf_json, wfForm: me }

                if (!me.stepLayout) {
                    me.stepLayout = me.wf_SetStepEdit.down('step_layout');
                }
                me.stepLayout.stepId = this.id;
                me.stepLayout.InitData(data);
            }
            // 打开窗体
            me.wf_SetStepEdit.show();
        }

        //子流程设置
        function subflowSetting() {
            var bbox = this.getBBox();
            alert('子流程设置')
        }

        rect.drag(move, dragger, up);
        if (me.wf_designer) {
            rect.click(click);
            if ("normal" == rect.type1) {
                rect.dblclick(stepSetting);
            }
            else if ("subflow" == rect.type1) {
                rect.dblclick(subflowSetting);
            }
        }
        me.wf_steps.push(rect);

        var text2 = text.length > 8 ? text.substr(0, 7) + "..." : text;
        var text1 = me.myWF.text(x + 52, y + 25, text2);
        text1.attr({ "font-size": "12px" });
        if (text.length > 8) text1.attr({ "title": text });
        text1.id = "text_" + id;
        text1.type1 = "text";
        me.wf_texts.push(text1);

        if (addToJSON == undefined || addToJSON == null) addToJSON = true;
        if (addToJSON) {
            var step = {};
            step.Id = guid;
            step.Type = type1 ? type1 : "normal";
            step.Name = text;
            step.Position_x = x;
            step.Position_y = y;

            if (rect.type1 == "normal") {
                step.OpinionDisplay = 1;
                step.ExpiredPrompt = 1;
                step.SignatureType = 0;
                step.WorkTime = "";
                step.LimitTime = "";
                step.OtherTime = "";
                step.Archives = 1;
                step.ArchivesParams = "";
                step.Note = "";
                step.DefaultUser = '';
                step.Forms = [];
                step.Buttons = [];
                step.FieldStatus = [];
                step.Event = {
                    SubmitBefore: "",
                    SubmitAfter: "",
                    BackBefore: "",
                    BackAfter: ""
                };
            }
            else if (rect.type1 == "subflow") {
                step.Flowid = "";
                step.Handler = "";
                step.Strategy = 0;
            }
            me.addStepData(step);
        }
    },
    // 添加步骤数据
    addStepData: function (step) {
        if (!step) return;
        var me = this;
        if (!me.wf_json.Steps) me.wf_json.Steps = [];
        var isup = false;
        for (var i = 0; i < me.wf_json.Steps.length; i++) {
            if (me.wf_json.Steps[i].Id == step.Id) {
                me.wf_json.Steps[i] = step;
                isup = true;
            }
        }
        if (!isup) {
            me.wf_json.Steps.push(step);
        }
    },
    //连接对象
    connObj: function (obj, addToJSON, title) {
        var me = this;
        if (addToJSON == undefined || addToJSON == null) addToJSON = true;

        if (me.isLine(obj)) {
            var newline = me.myWF.drawArr(obj);
            me.wf_conns.push(newline);
            if (addToJSON) {
                var line = {};
                line.Id = obj.Id;
                line.Text = title || "";
                line.FromID = obj.obj1.id;
                line.ToID = obj.obj2.id;
                me.addLine(line);
            }
            else {
                me.setLineText(obj.Id, title);
            }
        }
    },
    //判断一个节点与另一个节点之间是否可以连线
    isLine: function (obj) {
        var me = this;
        if (!obj || !obj.obj1 || !obj.obj2) {
            return false;
        }
        if (obj.obj1 === obj.obj2) {
            return false;
        }
        if (!me.isStepObj(obj.obj1) || !me.isStepObj(obj.obj2)) {
            return false;
        }
        if (me.wf_conns) {
            me.wf_conns.forEach(function (item, index) {
                if (obj.obj1 === obj.obj2 || (item.obj1 === obj.obj1 && item.obj2 === obj.obj2)) {
                    return false;
                }
            });
        }
        return true;
    },
    //判断一个对象是否是步骤对象
    isStepObj: function (obj) {
        return obj && obj.type1 && (obj.type1.toString() == "normal" || obj.type1.toString() == "subflow" || obj.type1.toString() == "judge");
    },
    //添加线
    addLine: function (line) {
        var me = this;
        if (!line || !line.FromID || !line.ToID) return;
        if (!me.wf_json.Lines) me.wf_json.Lines = [];
        var isup = false;
        for (var i = 0; i < me.wf_json.Lines.length; i++) {
            if (me.wf_json.Lines[i].Id == line.Id) {
                me.wf_json.Lines[i] = line;
                isup = true;
            }
        }
        if (!isup) {
            me.wf_json.Lines.push(line);
        }
        me.setLineText(line.Id, line.Text);
    },
    //随着节点位置的改变动态改变箭头
    drawArr: function () {
        var me = this;

        //连线单击事件
        function connClick() {
            for (var i = 0; i < me.wf_conns.length; i++) {
                if (me.wf_conns[i].arrPath === this) {

                    me.wf_conns[i].arrPath.attr({ "stroke": "#db0f14", "fill": "#db0f14" });
                }
                else {
                    me.wf_conns[i].arrPath.attr({ "stroke": me.wf_connColor, "fill": me.wf_connColor });
                }
            }
            for (var i = 0; i < me.wf_steps.length; i++) {
                me.wf_steps[i].attr("fill", "#efeff0");
                me.wf_steps[i].attr("stroke", "#23508e");
            }

            me.wf_focusObj = this;
        }

        //流转条件设置
        function connSetting() {
            var bbox = this.getBBox();
            if (!me.wf_SetLineEdit) {
                //获得 添加窗体对象
                var object = { winTitle: '连线设置', winWidth: 650, win: 'BPM.WFModel.Line.Layout', config: { wfData: me.wf_json, wfForm: me, lineId: this.id } };
                me.wf_SetLineEdit = util.createWindow(object);
            } else {
                var data = { wfData: me.wf_json, wfForm: me }

                if (!me.lineLayout) {
                    me.lineLayout = me.wf_SetLineEdit.down('line_layout');
                }
                me.lineLayout.lineId = this.id;
                me.lineLayout.InitData(data);
            }
            // 打开窗体
            me.wf_SetLineEdit.show();
        }

        Raphael.fn.drawArr = function (obj) {
            if (!obj || !obj.obj1) {
                return;
            }
            if (!obj.obj2) {
                var point1 = me.getStartEnd(obj.obj1, obj.obj2);
                var path2 = me.getArr(point1.start.x, point1.start.y, me.mouseX, me.mouseY, 7);
                for (var i = 0; i < me.tempArrPath.length; i++) {
                    me.tempArrPath[i].arrPath.remove();
                }
                me.tempArrPath = [];
                obj.arrPath = this.path(path2);
                obj.arrPath.attr({ "stroke-width": 2, "stroke": me.wf_connColor, "fill": me.wf_connColor });
                me.tempArrPath.push(obj);
                return;
            }

            var point = me.getStartEnd(obj.obj1, obj.obj2);
            var path1 = me.getArr(point.start.x, point.start.y, point.end.x, point.end.y, 7);
            try {
                if (obj.arrPath) {
                    obj.arrPath.attr({ path: path1 });
                }
                else {
                    obj.arrPath = this.path(path1);
                    obj.arrPath.attr({ "stroke-width": 1.7, "stroke": me.wf_connColor, "fill": me.wf_connColor, "x": point.start.x, "y": point.start.y, "x1": point.end.x, "y1": point.end.y });
                    if (me.wf_designer) {
                        obj.arrPath.click(connClick);
                        obj.arrPath.dblclick(connSetting);
                        obj.arrPath.id = obj.Id;
                        obj.arrPath.fromid = obj.obj1.id;
                        obj.arrPath.toid = obj.obj2.id;
                    }
                }
            } catch (e) { }

            return obj;
        }
    },
    //得到新步骤的XY
    getNewXY: function () {
        var me = this, x = 10, y = 50;
        if (me.wf_steps.length > 0) {
            var step = me.wf_steps[me.wf_steps.length - 1];
            x = parseInt(step.attr("x")) + 170;
            y = parseInt(step.attr("y"));
            if (x > me.myWF.width - me.wf_width) {
                x = 10;
                y = y + 100;
            }

            if (y > me.myWF.height - me.wf_height) {
                y = me.myWF.height - me.wf_height;
            }
        }
        return { x: x, y: y };
    },
    //得到GUID
    getGuid: function () {
        return Raphael.createUUID().toLowerCase();
    },
    // 设置线条内容
    setLineText: function (id, txt) {
        var me = this;
        var line;
        for (var i = 0; i < me.wf_conns.length; i++) {
            if (me.wf_conns[i].Id == id) {
                line = me.wf_conns[i];
                break;
            }
        }

        if (!line) {
            return;
        }
        var bbox = line.arrPath.getBBox();
        var txt_x = (bbox.x + bbox.x2) / 2;
        var txt_y = ((bbox.y + bbox.y2) / 2) - 10;

        var lineText = me.myWF.getById("line_" + id);
        if (lineText != null) {
            if (!txt) {
                lineText.remove();
            }
            else {
                lineText.attr("x", txt_x);
                lineText.attr("y", txt_y);
                lineText.attr("fill", '#066FBB');
                lineText.attr("font-size", '6');
                lineText.attr({ "text": txt || "" });
            }
            return;
        }
        if (txt) {
            var textObj = me.myWF.text(txt_x, txt_y, txt);
            textObj.attr("fill", '#066FBB');
            textObj.attr("font-size", '6');
            textObj.type1 = "line";
            textObj.id = "line_" + id;
            me.wf_texts.push(textObj);
        }
    },

    getStartEnd: function (obj1, obj2) {
        var bb1 = obj1 ? obj1.getBBox() : null;
        var bb2 = obj2 ? obj2.getBBox() : null;
        var p = [
                    { x: bb1.x + bb1.width / 2, y: bb1.y - 1 },
                    { x: bb1.x + bb1.width / 2, y: bb1.y + bb1.height + 1 },
                    { x: bb1.x - 1, y: bb1.y + bb1.height / 2 },
                    { x: bb1.x + bb1.width + 1, y: bb1.y + bb1.height / 2 },
                    bb2 ? { x: bb2.x + bb2.width / 2, y: bb2.y - 1 } : {},
                    bb2 ? { x: bb2.x + bb2.width / 2, y: bb2.y + bb2.height + 1 } : {},
                    bb2 ? { x: bb2.x - 1, y: bb2.y + bb2.height / 2 } : {},
                    bb2 ? { x: bb2.x + bb2.width + 1, y: bb2.y + bb2.height / 2 } : {}
        ];
        var d = {}, dis = [];
        for (var i = 0; i < 4; i++) {
            for (var j = 4; j < 8; j++) {
                var dx = Math.abs(p[i].x - p[j].x),
                            dy = Math.abs(p[i].y - p[j].y);
                if (
                        (i == j - 4) ||
                        (((i != 3 && j != 6) || p[i].x < p[j].x) &&
                        ((i != 2 && j != 7) || p[i].x > p[j].x) &&
                        ((i != 0 && j != 5) || p[i].y > p[j].y) &&
                        ((i != 1 && j != 4) || p[i].y < p[j].y))
                    ) {
                    dis.push(dx + dy);
                    d[dis[dis.length - 1]] = [i, j];
                }
            }
        }
        if (dis.length == 0) {
            var res = [0, 4];
        } else {
            res = d[Math.min.apply(Math, dis)];
        }
        var result = {};
        result.start = {};
        result.end = {};
        result.start.x = p[res[0]].x;
        result.start.y = p[res[0]].y;
        result.end.x = p[res[1]].x;
        result.end.y = p[res[1]].y;
        return result;
    },

    getArr: function (x1, y1, x2, y2, size) {
        var angle = Raphael.angle(x1, y1, x2, y2);
        var a45 = Raphael.rad(angle - 28);
        var a45m = Raphael.rad(angle + 28);
        var x2a = x2 + Math.cos(a45) * size;
        var y2a = y2 + Math.sin(a45) * size;
        var x2b = x2 + Math.cos(a45m) * size;
        var y2b = y2 + Math.sin(a45m) * size;
        return ["M", x1, y1, "L", x2, y2, "M", x2, y2, "L", x2b, y2b, "L", x2a, y2a, "z"].join(",");
    }

});