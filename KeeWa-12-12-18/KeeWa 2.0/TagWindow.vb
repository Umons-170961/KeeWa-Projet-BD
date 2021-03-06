﻿Imports System.Data.OleDb

Public Class TagWindow
    Private Sub TagWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loading()
    End Sub


    Private Sub BT_Supp_Click(sender As Object, e As EventArgs) Handles BT_Supp.Click
        Main.execute("DELETE FROM Tag WHERE Name = '" & CB_Modif.SelectedItem.ToString() & "'")
        loading()
    End Sub


    Private Sub BT_Sauv_Click(sender As Object, e As EventArgs) Handles BT_Sauv.Click
        If CB_Modif.Enabled = False Then
            Main.execute("INSERT INTO Tag (Name, Description) VALUES ('" & TB_Nom.Text() & "','" & RTB_Description.Text() & "')")
            If GarantyWindow.Visible() Then
                GarantyWindow.loading()
            End If
            Me.Close()
        Else
            Main.execute("UPDATE Tag SET Name = '" & TB_Nom.Text() & "', Description = '" & RTB_Description.Text() & "' WHERE Name ='" & CB_Modif.SelectedItem.ToString() & "'")
            loading()
        End If
    End Sub


    Private Sub CB_Modif_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_Modif.SelectedIndexChanged
        If CB_Modif.Items.Contains(CB_Modif.SelectedItem()) Then
            BT_Sauv.Enabled = True
            BT_Supp.Enabled = True
            Dim ObjetDataSet As DataSet = Main.GetInfo("SELECT * FROM Tag WHERE Name = '" & CB_Modif.SelectedItem.ToString() & "'", "Tag")
            TB_Nom.DataBindings.Clear()
            RTB_Description.DataBindings.Clear()
            TB_Nom.DataBindings.Add("Text", ObjetDataSet, "Tag.Name")
            RTB_Description.DataBindings.Add("Text", ObjetDataSet, "Tag.Description")
        Else
            BT_Sauv.Enabled = False
            BT_Supp.Enabled = False
        End If
    End Sub


    Sub loading()
        If LBL_Modif.Enabled Then
            BT_Sauv.Enabled = False
            BT_Supp.Enabled = False
            CB_Modif.Items.Clear()
            TB_Nom.Text() = ""
            RTB_Description.Text() = ""
            Main.Remplissage("SELECT Name FROM Tag", "Name", CB_Modif)
        Else
            BT_Supp.Enabled = True
            BT_Sauv.Enabled = True
        End If
    End Sub
End Class