#!/bin/bash
Models_folder_path="C:/Users/Novruzoff/source/repos/Sandal/EntityLayer/Concrete"

# Data Access Layer-də olan Abtract və Concrete folderları
DAL_Interface_folder_path="C:/Users/Novruzoff/source/repos/Sandal/DataAccessLayer/Abstract"
DAL_concrete_folder_path="C:/Users/Novruzoff/source/repos/Sandal/DataAccessLayer/Concrete"

# Business Layer-də olan Abtract və Concrete folderları
BL_Interface_folder_path="C:/Users/Novruzoff/source/repos/Sandal/BusinessLayer/Abstract"
BL_concrete_folder_path="C:/Users/Novruzoff/source/repos/Sandal/BusinessLayer/Concrete"

if [ ! -d "$DAL_Interface_folder_path" ]; then
    mkdir -p "$DAL_Interface_folder_path"
    echo "Data Access Layer abstract qovluq yaradıldı -> $DAL_Interface_folder_path"
else
    echo "Data Access Layer abstract qovluq mövcud -> $DAL_Interface_folder_path"
fi

if [ ! -d "$DAL_concrete_folder_path" ]; then
    mkdir -p "$DAL_concrete_folder_path"
    echo "Data Access Layer concrete qovluq yaradıldı -> $DAL_concrete_folder_path"
else
    echo "Data Access Layer concrete qovluq mövcud -> $DAL_concrete_folder_path"
fi

if [ ! -d "$BL_Interface_folder_path" ]; then
    mkdir -p "$BL_Interface_folder_path"
    echo "Busines Layer abstract qovluq yaradıldı -> $BL_Interface_folder_path"
else
    echo "Busines Layer abstract qovluq mövcud -> $BL_Interface_folder_path"
fi

if [ ! -d "$BL_concrete_folder_path" ]; then
    mkdir -p "$BL_concrete_folder_path"
    echo "Busines Layer concrete qovluq yaradıldı -> $BL_concrete_folder_path"
else
    echo "Busines Layer concrete qovluq mövcud -> $BL_concrete_folder_path"
fi

for file in "$Models_folder_path"/*; do

    # Class fayllarini toplamaq
    filename="$(basename "$file")"

    # Class-in adini almaq
    class_name="${filename%.*}"

    #! Data Access Layer

    new_crud_interface="I${class_name}Repository.cs"

    # Hemin class-dan Abstract Interface yaratmaq
    touch "$DAL_Interface_folder_path/$new_crud_interface"

    # Abstract Interface-in icini doldurmaq
    cat <<EOL >"$DAL_Interface_folder_path/$new_crud_interface"
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract;

public interface I${class_name}Repository : IGenericRepository<${class_name}>
{
}

EOL

    new_dal_concrete_class="${class_name}Repository.cs"

    # Hemin class-dan entityframework class yaratmaq
    touch "$DAL_concrete_folder_path/$new_dal_concrete_class"

    # EntityFramework Class-in icini doldurmaq
    cat <<EOL >"$DAL_concrete_folder_path/$new_dal_concrete_class"
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete;

public class ${class_name}Repository : GenericRepository<${class_name}>, I${class_name}Repository
{
    public ${class_name}Repository(SandalContext context) : base(context)
    {
    }
}


EOL

    echo "Created file at Data Access Layer: $DAL_Interface_folder_path/$new_crud_interface"
    echo "Created file at Data Access Layer: $DAL_concrete_folder_path/$new_dal_concrete_class"

    #! Business Layer

    new_service_interface="I${class_name}Service.cs"

    # Hemin class-dan Abstract Interface yaratmaq
    touch "$BL_Interface_folder_path/$new_service_interface"

    # Abstract Interface-in icini doldurmaq
    cat <<EOL >"$BL_Interface_folder_path/$new_service_interface"
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract;

public interface I${class_name}Service
{
    void Create${class_name}(${class_name} entity);
    void Update${class_name}(${class_name} entity);
    void Delete${class_name}(${class_name} entity);
    ${class_name} Get${class_name}ById(int id);
    List<${class_name}> GetAll${class_name}();
    Task<List<${class_name}>> GetAll${class_name}Async();
}

EOL

    new_bl_concrete_class="${class_name}Service.cs"

    # Hemin class-dan entityframework class yaratmaq
    touch "$BL_concrete_folder_path/$new_bl_concrete_class"

    # EntityFramework Class-in icini doldurmaq
    cat <<EOL >"$BL_concrete_folder_path/$new_bl_concrete_class"
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete;

public class ${class_name}Service : I${class_name}Service
{
    private readonly I${class_name}Repository _${class_name}Repository;

    public ${class_name}Service(I${class_name}Repository ${class_name}Repository)
    {
        _${class_name}Repository = ${class_name}Repository;
    }

    public void Create${class_name}(${class_name} entity)
    {
        _${class_name}Repository.Insert(entity);
    }

    public void Delete${class_name}(${class_name} entity)
    {
        _${class_name}Repository.Delete(entity);
    }

    public List<${class_name}> GetAll${class_name}()
    {
        return _${class_name}Repository.GetAll().ToList();
    }

    public async Task<List<${class_name}>> GetAll${class_name}Async()
    {
        return await _${class_name}Repository.GetAllAsync();
    }

    public ${class_name} Get${class_name}ById(int id)
    {
        return _${class_name}Repository.GetById(id);
    }

    public void Update${class_name}(${class_name} entity)
    {
        _${class_name}Repository.Update(entity);
    }
}


EOL

    echo "Created file at Business Layer: $BL_Interface_folder_path/$new_service_interface"
    echo "Created file at Business Layer: $BL_concrete_folder_path/$new_bl_concrete_class"
done

sleep 10
