using System.Collections.Generic;
using System.Linq;


namespace MyWebApp.Models
{
    public class PaginationViewModel<T>
    {
        public readonly PageViewModel _pageViewModel;
        public readonly List<T> _modelList;

        public PaginationViewModel(PageViewModel pageViewModel, List<T> modelList)
        {
            this._pageViewModel = pageViewModel;
            this._modelList = modelList;            
        }        
    }
}