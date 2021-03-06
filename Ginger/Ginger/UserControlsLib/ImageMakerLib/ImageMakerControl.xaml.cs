﻿#region License
/*
Copyright © 2014-2018 European Support Limited

Licensed under the Apache License, Version 2.0 (the "License")
you may not use this file except in compliance with the License.
You may obtain a copy of the License at 

http://www.apache.org/licenses/LICENSE-2.0 

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and 
limitations under the License. 
*/
#endregion

using Amdocs.Ginger.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FontAwesome.WPF;
using Amdocs.Ginger.Common.Enums;

namespace Amdocs.Ginger.UserControls
{
    /// <summary>
    /// Interaction logic for ImageMaker.xaml
    /// </summary>
    public partial class ImageMakerControl : UserControl
    {
        // Icon Property
        // We list all avaible icons for Ginger, this icons can be resized and will automatically match
        public static readonly DependencyProperty ImageTypeProperty = DependencyProperty.Register("ImageType", typeof(eImageType), typeof(ImageMakerControl),
                        new FrameworkPropertyMetadata(eImageType.Ginger, OnIconPropertyChanged));
               
        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageMakerControl IMC = (ImageMakerControl)d;
            IMC.SetImage();       
        }
                
        public eImageType ImageType
        {
            get { return (eImageType)GetValue(ImageTypeProperty); }
            set
            {
                SetValue(ImageTypeProperty, value);
                SetImage();
            }
        }

        public static ImageSource GetImage(eImageType imageType,double SetAsFontImageWithSize=0.0,double width=0.0,bool SetBorder=false)
        {
            ImageSource Source = null;

            ImageMakerControl IM = new ImageMakerControl();
            IM.ImageType = imageType;
            IM.SetAsFontImageWithSize = SetAsFontImageWithSize;
            IM.Width = width;
            IM.SetBorder = SetBorder;
            
            if (IM.xStaticImage.Source != null)
                Source = IM.xStaticImage.Source;
            else if (IM.xFAImage.Source != null)
                Source = IM.xFAImage.Source;
           
            return Source;
        }
        
        //Font Size Property
        //if this value is set then instead of showing image will show FAFont and set its FontSize 
        public static readonly DependencyProperty SetAsFontImageWithSizeProperty = DependencyProperty.Register("SetAsFontImageWithSize", typeof(double), typeof(ImageMakerControl), new FrameworkPropertyMetadata(0.0, OnIconPropertyChanged));
        public double SetAsFontImageWithSize
        {
            get
            {
                return (double)GetValue(SetAsFontImageWithSizeProperty);
            }
            set
            {
                SetValue(SetAsFontImageWithSizeProperty, value);
                SetImage();
            }
        }

        public static readonly DependencyProperty SetImageMakerBorderProperty = DependencyProperty.Register("SetBorder", typeof(bool), typeof(ImageMakerControl), new FrameworkPropertyMetadata(false, OnIconPropertyChanged));
        public bool SetBorder
        {
            get
            {
                return (bool)GetValue(SetImageMakerBorderProperty);
            }
            set
            {
                SetValue(SetImageMakerBorderProperty, value);
                SetImage();
            }
        }

        public static readonly DependencyProperty SetImageMakerEnableProperty = DependencyProperty.Register("Enabled", typeof(bool), typeof(ImageMakerControl), new FrameworkPropertyMetadata(true, OnIconPropertyChanged));
        public bool Enabled
        {
            get
            {
                return (bool)GetValue(SetImageMakerEnableProperty);
            }
            set
            {
                SetValue(SetImageMakerEnableProperty, value);
                SetImage();
            }
        }

        public static readonly DependencyProperty SetImageMakerForegroundProperty = DependencyProperty.Register("ImageForeground", typeof(SolidColorBrush), typeof(ImageMakerControl), new FrameworkPropertyMetadata(null, OnIconPropertyChanged));
        public SolidColorBrush ImageForeground
        {
            get
            {
                return (SolidColorBrush)GetValue(SetImageMakerForegroundProperty);
            }
            set
            {
                SetValue(SetImageMakerForegroundProperty, value);
                SetImage();
            }
        }

        public ImageMakerControl()
        {
            InitializeComponent();
        }
       
        private void SetImage()
        {
            ResetImageView();
            switch (ImageType)
            {
                #region General Images
                //############################## General Images:
                case eImageType.Empty:
                    // Do nothing and leave it empty
                    // SetAsFontAwesomeIcon(FontAwesomeIcon.Ban);                    
                    break;
                case eImageType.Ginger:
                    SetAsStaticImage("Ginger.png");
                    break;
                #endregion


                #region Repository Items Images
                //############################## Repository Items Images:
                case eImageType.Solution:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Database);
                    break;
                case eImageType.BusinessFlow:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Sitemap);
                    break;
                case eImageType.Activity:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ThList);
                    break;
                case eImageType.Action:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Bolt);
                    break;
                case eImageType.Agent:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.User);
                    break;
                case eImageType.RunSet:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.PlayCircle);
                    break;                             
                //TODO: remove 16/32 need to have only one 
                case eImageType.APIModel16:
                    SetAsStaticImage("ApiModel_16x16.png");
                    break;
                case eImageType.APIModel32:
                    SetAsStaticImage("ApiModel_32x32.png");
                    break;
                case eImageType.Runner:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.PlayCircleOutline);
                    break;
                case eImageType.Operations:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Cogs);
                    break;
                case eImageType.Environment:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Globe);
                    break;
                case eImageType.Parameter:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Sliders);
                    break;
                case eImageType.Application:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.WindowMaximize);
                    break;
                case eImageType.HtmlReport:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Html5);
                    break;
                #endregion


                #region Execution Status Images
                //############################## Execution Status Images:
                case eImageType.Passed:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.CheckCircle, (SolidColorBrush)FindResource("$PassedStatusColor"), 0, "Passed");
                    break;
                case eImageType.Failed:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.TimesCircle, (SolidColorBrush)FindResource("$FailedStatusColor"), 0, "Failed");
                    break;
                case eImageType.Pending:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ClockOutline, (SolidColorBrush)FindResource("$PendingStatusColor"), 0, "Pending");
                    break;
                case eImageType.Processing:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Spinner, (SolidColorBrush)FindResource("$ProcessingColor"), 2);
                    break;
                case eImageType.Ready:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ThumbsOutlineUp, (SolidColorBrush)FindResource("$PendingStatusColor"));
                    break;
                case eImageType.Stopped:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.StopCircle, (SolidColorBrush)FindResource("$StoppedStatusColor"), 0, "Stopped");
                    break;
                case eImageType.Blocked:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Ban, (SolidColorBrush)FindResource("$BlockedStatusColor"), 0, "Blocked");
                    break;
                case eImageType.Skipped:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.MinusCircle, (SolidColorBrush)FindResource("$SkippedStatusColor"), 0, "Skipped");
                    break;
                case eImageType.Running:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Spinner, (SolidColorBrush)FindResource("$RunningStatusColor"), 2, "Running");
                    break;
                #endregion


                #region Operations Images
                //############################## Operations Images:
                case eImageType.Add:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Plus);
                    break;
                case eImageType.Refresh:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Refresh);
                    break;
                case eImageType.Config:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Cog);
                    break;
                case eImageType.Edit:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.PencilSquareOutline);
                    break;
                case eImageType.Save:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Save);
                    break;
                case eImageType.Reply:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Reply);
                    break;
                case eImageType.Stop:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Stop);
                    break;
                case eImageType.Play:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Play);
                    break;
                case eImageType.StopCircleOutline:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.StopCircleOutline);
                    break;
                case eImageType.PlayCircleOutline:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.PlayCircleOutline);
                    break;
                case eImageType.Close:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Close);
                    break;
                case eImageType.Continue:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.FastForward);
                    break;
                case eImageType.Open:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.FolderOutlinepenOutline);
                    break;
                case eImageType.New:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Magic);
                    break;
                case eImageType.Analyze:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Stethoscope);
                    break;
                case eImageType.GoBack:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ArrowLeft);
                    break;
                case eImageType.GoNext:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ArrowRight);
                    break;
                case eImageType.Finish:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.FlagCheckered);
                    break;
                case eImageType.Cancel:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Close);
                    break;
                case eImageType.Reset:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.RotateLeft);
                    break;
                case eImageType.Delete:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Trash);
                    break;
                case eImageType.DeleteSingle:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Minus);
                    break;
                case eImageType.Minimize:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.WindowMinimize);
                    break;
                case eImageType.MoveRight:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ArrowRight);
                    break;
                case eImageType.MoveLeft:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ArrowLeft);
                    break;
                case eImageType.MoveUp:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ArrowUp);
                    break;
                case eImageType.MoveDown:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ArrowDown);
                    break;
                case eImageType.Reorder:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Reorder);
                    break;
                case eImageType.Retweet:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Retweet);
                    break;
                case eImageType.Automate:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Certificate);
                    break;
                case eImageType.ParallelExecution:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Random);
                    break;
                case eImageType.SequentialExecution:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.SortNumericAsc);
                    break;               
                case eImageType.Search:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Search);                    
                    break;
                case eImageType.Duplicate:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.FilesOutline);                    
                    break;
                case eImageType.Merge:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ObjectGroup);
                    break;
                case eImageType.Sync:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Link);
                    break;
                case eImageType.UnSync:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Unlink);
                    break;
                case eImageType.Visible:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Eye);
                    break;
                case eImageType.Invisible:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.EyeSlash);
                    break;
                case eImageType.View:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Search);
                    break;
                case eImageType.Expand:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ChevronDown);
                    break;
                case eImageType.Collapse:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ChevronUp);
                    break;
                case eImageType.ExpandAll:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.AngleDoubleDown);
                    break;
                case eImageType.CollapseAll:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.AngleDoubleUp);
                    break;
                case eImageType.ActiveAll:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Check);
                    break;
                case eImageType.Info:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.InfoCircle);
                    break;
                case eImageType.Export:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ShareAlt);
                    break;
                case eImageType.Times:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Times);
                    break;
                case eImageType.Times_Red:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Times, (SolidColorBrush)FindResource("$RedColor"), 0, "ToolTip");
                    break;
                case eImageType.Exchange:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Exchange);
                    break;
                case eImageType.Share:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ShareAlt);
                    break;
                case eImageType.ShareExternal:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ShareSquareOutline);
                    break;
                case eImageType.Download:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Download);
                    break;
                #endregion


                #region Items Images
                //############################## Items Images:
                case eImageType.KidsDrawing:
                    xViewBox.Visibility = Visibility.Visible;
                    xViewBox.Child = GetKidsDrawingShape();
                    break;
                case eImageType.FlowDiagram:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Sitemap);
                    break;
                case eImageType.List:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Table);
                    break;
                case eImageType.File:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.FileOutline);
                    break;
                case eImageType.Folder:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.FolderOutline);
                    break;
                case eImageType.OpenFolder:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.FolderOutlinepenOutline);
                    break;
                case eImageType.EllipsisH:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.EllipsisH);
                    break;                
                case eImageType.ListGroup:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ListUl);
                    break;
                case eImageType.Sitemap:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Sitemap);
                    break;
                case eImageType.ItemModified:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Asterisk, Brushes.OrangeRed, 5, "Item was modified");
                    break;
                case eImageType.Clock:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ClockOutline);
                    break;
                case eImageType.Link:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ExternalLink);
                    break;
                case eImageType.Report:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.PieChart);
                    break;
                case eImageType.Active:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ToggleOn);
                    break;
                case eImageType.InActive:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.ToggleOff);
                    break;
                case eImageType.History:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.History);
                    break;
                case eImageType.SourceControlNew:
                    SetAsStaticImage("SourceControlItemAdded_10x10.png");
                    break;
                case eImageType.SourceControlModified:
                    SetAsStaticImage("SourceControlItemChange_10x10.png");
                    break;
                case eImageType.SourceControlDeleted:
                    SetAsStaticImage("SourceControlItemDeleted_10x10.png");
                    break;
                case eImageType.SourceControlEquel:
                    SetAsStaticImage("SourceControlItemUnchanged_10x10.png");
                    break;
                case eImageType.SourceControlLockedByAnotherUser:
                    SetAsStaticImage("Lock_Red_10x10.png");
                    break;
                case eImageType.SourceControlLockedByMe:
                    SetAsStaticImage("Lock_Yellow_10x10.png");
                    break;
                case eImageType.Check:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.CheckCircleOutline);
                    break;
                case eImageType.Bug:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Bug);
                    break;
                case eImageType.DataSource:
                    SetAsStaticImage("DataSource_16x16.png");
                    break;
                case eImageType.PluginPackage:
                    SetAsStaticImage("Plugin_32x32.png");
                    break;
                #endregion


                default:
                    SetAsFontAwesomeIcon(FontAwesomeIcon.Question, Brushes.Red);
                    this.Background = Brushes.Yellow;
                    break;
            }
        }
        
        private void ResetImageView()
        {
            // Reset All do defaults
            xFAImage.Visibility = Visibility.Collapsed;
            xFAImage.Spin = false;
            xFAFont.Visibility = Visibility.Collapsed;
            xFAFont.Spin = false;
            xStaticImage.Visibility = Visibility.Collapsed;
            xViewBox.Visibility = Visibility.Collapsed;
            this.Background = null;
        }

        private void SetAsFontAwesomeIcon(FontAwesomeIcon fontAwesomeIcon, SolidColorBrush foreground = null, double spinDuration = 0, string toolTip = null)
        {
            //set the icon
            xFAImage.Icon = fontAwesomeIcon;
            xFAFont.Icon = fontAwesomeIcon;
            
            if (this.ImageForeground != null)
                foreground = (SolidColorBrush)this.ImageForeground;

            //set Foreground
            if (foreground != null)            
                xFAImage.Foreground = foreground;            
            else            
                xFAImage.Foreground = (SolidColorBrush)FindResource("$DarkBlue");

            if (spinDuration != 0)
            {
                xFAImage.Spin = true;
                xFAImage.SpinDuration = spinDuration;
                xFAFont.Spin = true;
                xFAFont.SpinDuration = spinDuration;
            }

            if(!string.IsNullOrEmpty(toolTip))
            {
                xFAImage.ToolTip = toolTip;                
                xFAFont.ToolTip = toolTip;
            }
            //set which type to show Image/Font
            if (SetAsFontImageWithSize > 0)
            {
                xFAFont.Visibility = Visibility.Visible;
                xFAFont.FontSize = SetAsFontImageWithSize;
            }
            else
            {
                xFAImage.Visibility = Visibility.Visible;
            }
            if (SetBorder)
            {
                ImageMakerBorder.BorderThickness = new Thickness(1);
                ImageMakerBorder.BorderBrush = (SolidColorBrush)FindResource("$DarkBlue");
            }
            else
            {
                ImageMakerBorder.BorderThickness = new Thickness(0);
            }
        }

        private BitmapImage GetImageBitMap(string imageName)
        {
            return new BitmapImage(new Uri("pack://application:,,,/Ginger;component/UserControlsLib/ImageMakerLib/Images/" + imageName));
        }

        private void SetAsStaticImage(string imageName = "", BitmapImage imageBitMap = null)
        {
            xStaticImage.Visibility = Visibility.Visible;
            if (imageBitMap != null)
                xStaticImage.Source = imageBitMap;
            else
                xStaticImage.Source = GetImageBitMap(imageName);
        }
        
        Shape GetKidsDrawingShape()
        {
            Path path = new Path();

            path.Width = 500;
            path.Height = 500;

            path.StrokeThickness = 5;
            path.Stroke = Brushes.Purple;

            string PathData = "M 100,200 C 100,25 400,350 400,175 H 280";

            path.Data = Geometry.Parse(PathData);

            return path;
        }

        internal void StopImageSpin()
        {
            //Used for visual compare, we stop the spinner so image compare will be able to compare
            xFAImage.Spin = false;
            xFAFont.Spin = false;
        }
    }
}
