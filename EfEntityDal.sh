#!/bin/bash
Models_folder_path="C:/Users/Novruzoff/source/repos/Sandal/EntityLayer/Concrete"

Ef_folder_path="C:/Users/Novruzoff/source/repos/Sandal/DataAccessLayer/EntityFramework"
Interface_folder_path="C:/Users/Novruzoff/source/repos/Sandal/DataAccessLayer/Abstract"

for file in "$Models_folder_path"/*
do
    filename="$(basename "$file")"                  # Class fayllarini toplamaq
    class_name="${filename%.*}"                     # Class-in adini almaq
    new_crud_interface="I${class_name}Dal.cs"   
    touch "$Interface_folder_path/$new_crud_interface"           # Hemin class-dan Abstract Interface yaratmaq
    cat <<EOL > "$Interface_folder_path/$new_crud_interface"     # Abstract Interface-in icini doldurmaq
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface I${class_name}Dal : IGenericDal<${class_name}>
    {
    }
}

EOL
    
    new_ef_class="Ef${class_name}Dal.cs"            
    touch "$Ef_folder_path/$new_ef_class"           # Hemin class-dan entityframework class yaratmaq

    cat <<EOL > "$Ef_folder_path/$new_ef_class"     # EntityFramework Class-in icini doldurmaq
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class Ef${class_name}Dal : GenericRepository<${class_name}>, I${class_name}Dal
    {
        public Ef${class_name}Dal(SandalContext context) : base(context)
        {
        }
    }
}

EOL

    echo "Created file: $Ef_folder_path/$new_ef_class"
done

sleep 10