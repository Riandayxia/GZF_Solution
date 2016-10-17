function _0e55416f09916815b87b3caa18d368756ca15b51() { };/*公共配置类*/
Ext.define('config', {
    extend: 'app.config',
    alternateClassName: 'config',
    statics: {
        str: {
            //登录注册
            Registered: '注册帐号',
            Register: '注册',
            Retrieve: '忘记密码',
            loginTitle: '登录',
            loginNameTip: '昵称/手机号',
            loginPwdTip: '请输入密码',
            LoginStatus: '登录中....',
            Reset: '重置密码',
            //家政服务
            Housekeeping: '家政服务',
            PartTimeJob: '在线预约—钟点工',
            YueSao: '在线预约—月嫂',
            CareWorkers: '在线预约—护工',
            HomeAunt: '在线预约—住家阿姨',
            Houseprotocol:'《智慧公租房家政服务协议》',
            //账户管理
            MyAccount:'我的账户',
            AddressList: '地址管理',
            AddressEdit: '编辑地址',
            AddressDetails: '地址详细',

            HomeArea: '首页',
            WorkArea: '工作区',
            Wares: '商城',
            Finance: '金融',
            InfoAre: '信息栏',
            MiArea: '我的',
            PressExitApp: '再按一次退出程序',
            Insert: '添 加',
            Update: '修 改',
            Delete: '删 除',
        },
        // 登录用户
        LoginUser: {
            Name: '',
            LoginName: '',
            Id: ''
        },
        // 系统启动页面
        MainPage: 'QST.Main.Layout',//'QST.Main.Layout',
        // 登录界面
        LoginPage: 'QST.Main.Login',
        // 当前登录时间
        CurrentTime: 0,
        // 过期时间
        Timeout: 15 * 60,
        // 权限菜单
        powers: ['QST.Main.Menu', 'QST.Main.Infobar', 'QST.Main.SetList'],
        // 用户信息及系统信息
        idata: {},
        // 是否具有配置流程权限
        IsWFPower: function () {
            return true;
        },
        url: 'http://localhost:6206',
        // url: 'http://mobile.cnsuhui.com',
        javaScriptUrl: ''//js远程加载地址 为空默认加载本域js
    }
})