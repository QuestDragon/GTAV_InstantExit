﻿using GTA;
using GTA.Native;
using GTA.Math;
using System.Windows.Forms;
using System;
using Hash = GTA.Native.Hash;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Reflection;

// Generated by ChatGPT

public class InstantExit : Script
{
    private Keys main_key; //キー設定を用意
    private Keys mod_key; //キー設定を用意
    private bool is_debug; //デバッグモードの有無
    private bool auto_invincible; //落下時無敵モードの有無
    private bool equip_parachute; //パラシュートの自動取得
    private bool press_main = false;
    private bool press_mod = false;
    private bool script_processing = false; //スクリプトが動作しているか(無敵移行時に使用する）
    private bool ScriptInvincible = false; //無敵モードがこのスクリプトによって有効か

    //バージョン情報
    private static AssemblyName assembly = Assembly.GetExecutingAssembly().GetName(); //アセンブリ情報
    private string ver = assembly.Version.ToString(3); // 形式は0.0.0

    public InstantExit()
    {
        ScriptSettings ini = ScriptSettings.Load(@"scripts\InstantExit.ini"); //INI File
        // iniのデータを読み込む (セクション、キー、デフォルト値)
        main_key = ini.GetValue<Keys>("Settings", "InstantExitKey", Keys.F);
        mod_key = ini.GetValue<Keys>("Settings", "ModifierKey", Keys.LShiftKey);
        equip_parachute = ini.GetValue<bool>("Settings","Parachute",true);
        auto_invincible = ini.GetValue<bool>("Settings","Invincible",false);
        is_debug = ini.GetValue<bool>("Settings","DebugMode",false);

        press_main = false;
        press_mod = false;

        // イベント登録
        if (auto_invincible) //無敵モードを使う場合のみTick有効
        {
            Tick += OnTick;
        }
        
        KeyDown += keyDown;
        KeyUp += keyUp;

        Debug_Notification($"~o~InstantExit ~q~v{ver}~s~ is ~g~ready!");
    }

    private async void DoExit()
    {
        Ped player = Game.Player.Character;

        // プレイヤーが車に乗っているか確認
        if (player.IsInVehicle())
        {

            // 乗っている車両を取得
            Vehicle vehicle = player.CurrentVehicle;

            // Function.Callを知るなら… https://nativedb.dotindustries.dev/gta5/natives
            // 各関数はHashに格納されている。そこから使いたいファンクションを記述する。
            // 関数が必要とする引数は括弧でくくる必要はなく、カンマでそのまま必要数入力していくだけで良い。
            // 関数が必要とする引数の詳細は上記ウェブサイトで確認できる。

            if (equip_parachute)
            {
                // パラシュートを追加（もし持っていなければ）
                if (!Function.Call<bool>(Hash.HAS_PED_GOT_WEAPON, player, WeaponHash.Parachute, false))
                {
                    Debug_Notification("Parachute equipped!");
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, player, WeaponHash.Parachute, 1, false, true);
                }
            }


            // 車両の停止を無視して即座に降りられるように設定
            Function.Call(Hash.TASK_LEAVE_VEHICLE, player, vehicle, 4160); // フラグ4160は「強制降車」を意味する

            // 車から降りた後のプレイヤー位置を少し調整（空中で降りても安定）
            player.Position = vehicle.Position + new Vector3(0, 0, 2); // 位置を少し上に移動

            Debug_Notification("Exited!");
            if(auto_invincible && !Game.Player.IsInvincible) // 他のMODやチートによって無敵になっていない場合
            {
                Game.Player.IsInvincible = true; // 無敵モードをオン
                ScriptInvincible = true; //スクリプトによる無敵をオン
                Debug_Notification("Invincible!");
                await Task.Delay(200); //RagdollやFallingを待機
                script_processing = true; //Tick Start
            }


            // パラシュートを自動的に装備(視覚的）
            // Function.Call(Hash.SET_PED_COMPONENT_VARIATION, player, 8, 15, 0, 0);
        }
    }

    [DllImport("user32.dll")]
    private static extern short GetKeyState(int nVirtKey); //仮想キーコードからのキー状態取得メソッド
    private void LRkeyUp()
    {
        #region ShiftKey
        if (GetKeyState(0xA0) >= 0) // LShiftKey
        {
            key_state_changer(Keys.LShiftKey,false);
        }
        if (GetKeyState(0xA1) >= 0) // RShiftKey
        {
            key_state_changer(Keys.RShiftKey, false);
        }
        #endregion
        #region ControlKey
        if (GetKeyState(0xA2) >= 0) // LControlKey
        {
            key_state_changer(Keys.LControlKey, false);
        }
        if (GetKeyState(0xA3) >= 0) // RControlKey
        {
            key_state_changer(Keys.RControlKey, false);
        }
        #endregion
        #region AltKey
        if (GetKeyState(0xA4) >= 0) // LMenu
        {
            key_state_changer(Keys.LMenu, false);
        }
        if (GetKeyState(0xA5) >= 0) // RMenu
        {
            key_state_changer(Keys.RMenu, false);
        }
        #endregion

    }

    private void LRkeyDown()
    {
        #region ShiftKey
        if (GetKeyState(0xA0) < 0) // LShiftKey
        {
            key_state_changer(Keys.LShiftKey, true);
        }
        if (GetKeyState(0xA1) < 0) // RShiftKey
        {
            key_state_changer(Keys.RShiftKey, true);
        }
        #endregion
        #region ControlKey
        if (GetKeyState(0xA2) < 0) // LControlKey
        {
            key_state_changer(Keys.LControlKey, true);
        }
        if (GetKeyState(0xA3) < 0) // RControlKey
        {
            key_state_changer(Keys.RControlKey, true);
        }
        #endregion
        #region AltKey
        if (GetKeyState(0xA4) < 0) // LMenu
        {
            key_state_changer(Keys.LMenu, true);
        }
        if (GetKeyState(0xA5) < 0) // RMenu
        {
            key_state_changer(Keys.RMenu, true);
        }
        #endregion

    }

    private void key_state_changer(Keys k,bool pressed)
    {
        if(k == main_key)
        {
            press_main = pressed;
            Debug_Notification("Main key: " + pressed);
        }
        if(k == mod_key)
        {
            press_mod = pressed;
            Debug_Notification("Modifier key: " + pressed);
        }
    }

    private void keyUp(object sender, KeyEventArgs e)
    {
        key_state_changer(e.KeyCode, false);

        if (new Keys[] { Keys.ControlKey, Keys.ShiftKey, Keys.Menu }.Contains(e.KeyCode)) // Ctrl, Alt ,Shiftの場合 (下でelseを使わない理由はLR指定をしていない場合の対策)
        {
            LRkeyUp();
        }
    }

    private void keyDown(object sender, KeyEventArgs e)
    {
        if (new Keys[] { Keys.ControlKey, Keys.ShiftKey, Keys.Menu }.Contains(e.KeyCode)) // Ctrl, Alt ,Shiftの場合 (下でelseを使わない理由はLR指定をしていない場合の対策)
        {
            LRkeyDown();
        }
        if (e.KeyCode != Keys.Escape) //ESCはゲームがポーズして押しっぱなし判定になってしまうため除外
        {
            key_state_changer(e.KeyCode, true);
        }

        Vehicle cv = Game.Player.Character.CurrentVehicle; //乗車中の車両情報

        if (cv != null && press_main) //車に乗っていて、設定したキーバインドが押されている
        {
            //修飾キーが押されているか（ない場合は普通に実行）
            if (mod_key == Keys.None | press_mod)
            {
                DoExit();
            }
        }
    }

    private async void OnTick(object sender, EventArgs e)
    {
        Ped player = Game.Player.Character;

        if(is_debug)
        {
            GTA.UI.Screen.ShowSubtitle($"~o~InstanceExit: {ScriptInvincible}~s~ - rag:{player.IsRagdoll} fall: {player.IsFalling} PState: {player.ParachuteState}");
        }
        

        if(!script_processing)
        {
            return; //スクリプトが作動していない場合はこの先を実行しない
        }

        // 着地した場合
        if (ScriptInvincible && !player.IsRagdoll && !player.IsFalling && player.ParachuteState == ParachuteState.None)
        {
            script_processing = false; //Tick動作終了

            await Task.Delay(100); //ダメージが来た瞬間に無敵を解除しないよう少し待機

            Game.Player.IsInvincible = false; // 無敵モードをオフ
            ScriptInvincible = false;
            Debug_Notification("No longer invincible!");
        }
    }

    private void Debug_Notification(string message)
    {
        if (is_debug)
        {
            GTA.UI.Notification.Show("[InstanceExit] " + message);
        }
    }
}
