namespace TiendaVirtualMVC.Web.Pagination
{
    using System;
    using System.Collections.Generic;

    using TiendaVirtualMVC.Web.Models;

    public class PagedList : List<Producto>
    {
        public PagedList(IEnumerable<Producto> productos,
                         int totalItems, int currenPage,
                        int pageSize)
        {
            this.AddRange(productos);
            this.TotalItems = totalItems;
            this.CurrentPage = currenPage;
            this.ItemsPerPage = pageSize;
        }

        public int TotalItems { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public bool HasPreviusPage
        {
            get
            {
                return CurrentPage > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return CurrentPage < TotalPages;
            }
        }

        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling(
                    (decimal)this.TotalItems / this.ItemsPerPage);
            }
        }
    }
}