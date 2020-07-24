using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingHood
{
    class ObjectStudy 
    {
        public void StudyObject(Object obj) {
            var events = obj.GetType().GetEvents();

            MethodInfo mi = this.GetType().GetMethod("Handle");
            foreach (var ev in events)
            {


                System.Diagnostics.Debug.WriteLine(ev.Name);
                Type handlerType = ev.EventHandlerType;
                
                Delegate d = Delegate.CreateDelegate(handlerType, mi);
                ev.AddEventHandler(obj, d);
                //ev.AddEventHandler(obj, (d,e) => { });
            }
        }

        internal void StudyTextBox(TextBox searchBox)
        {
            searchBox.Leave += SearchBox_Leave;
            searchBox.Enter += SearchBox_Enter;
            searchBox.GotFocus += SearchBox_GotFocus;
            searchBox.LostFocus += SearchBox_LostFocus;
        }

        private void SearchBox_LostFocus(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Lost focus!");
        }

        private void SearchBox_GotFocus(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Got focus!");
        }

        private void SearchBox_Enter(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Search enter!");
        }

        private void SearchBox_Leave(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Search leave!");
        }

        public void Handle(object sender, EventArgs e)
        {            
            System.Diagnostics.Debug.WriteLine(  e.ToString());
        }

        private void FormEventHandler(string evName, object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(evName + " TRIGGERED !!");
        }

        public void StudyFormEvents(Form form)
        {
            form.Activated                += (s, e) => { FormEventHandler("Activated", s, e);                };
            form.AutoSizeChanged          += (s, e) => { FormEventHandler("AutoSizeChanged", s, e);          };
            form.AutoValidateChanged      += (s, e) => { FormEventHandler("AutoValidateChanged", s, e);      };
            form.Closed                   += (s, e) => { FormEventHandler("Closed", s, e);                   };
            form.Closing                  += (s, e) => { FormEventHandler("Closing", s, e);                  };
            form.Deactivate               += (s, e) => { FormEventHandler("Deactivate", s, e);               };
            form.FormClosed               += (s, e) => { FormEventHandler("FormClosed", s, e);               };
            form.FormClosing              += (s, e) => { FormEventHandler("FormClosing", s, e);              };
            form.HelpButtonClicked        += (s, e) => { FormEventHandler("HelpButtonClicked", s, e);        };
            form.InputLanguageChanged     += (s, e) => { FormEventHandler("InputLanguageChanged", s, e);     };
            form.InputLanguageChanging    += (s, e) => { FormEventHandler("InputLanguageChanging", s, e);    };
            form.Load                     += (s, e) => { FormEventHandler("Load", s, e);                     };
            form.MarginChanged            += (s, e) => { FormEventHandler("MarginChanged", s, e);            };
            form.MaximizedBoundsChanged   += (s, e) => { FormEventHandler("MaximizedBoundsChanged", s, e);   };
            form.MaximumSizeChanged       += (s, e) => { FormEventHandler("MaximumSizeChanged", s, e);       };
            form.MdiChildActivate         += (s, e) => { FormEventHandler("MdiChildActivate", s, e);         };
            form.MenuComplete             += (s, e) => { FormEventHandler("MenuComplete", s, e);             };
            form.MenuStart                += (s, e) => { FormEventHandler("MenuStart", s, e);                };
            form.MinimumSizeChanged       += (s, e) => { FormEventHandler("MinimumSizeChanged", s, e);       };
            form.ResizeBegin              += (s, e) => { FormEventHandler("ResizeBegin", s, e);              };
            form.ResizeEnd                += (s, e) => { FormEventHandler("ResizeEnd", s, e);                };
            form.RightToLeftLayoutChanged += (s, e) => { FormEventHandler("RightToLeftLayoutChanged", s, e); };
            form.Shown                    += (s, e) => { FormEventHandler("Shown", s, e);                    };
            form.TabIndexChanged          += (s, e) => { FormEventHandler("TabIndexChanged", s, e);          };
            form.TabStopChanged           += (s, e) => { FormEventHandler("TabStopChanged", s, e);           };
        }

    }
}
