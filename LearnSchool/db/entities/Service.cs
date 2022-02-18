namespace LearnSchool.db
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Table("Service")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            ClientServices = new HashSet<ClientService>();
            ServicePhotoes = new HashSet<ServicePhoto>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public decimal Cost { get; set; }

        public int DurationInSeconds { get; set; }

        [StringLength(1073741823)]
        public string Description { get; set; }

        public double? Discount { get; set; }

        [StringLength(1000)]
        public string MainImagePath { get; set; }

        [NotMapped]
        public byte[] MainImage
        {
            get
            {
                try
                {
                    return File.ReadAllBytes(MainImagePath);
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    if (File.Exists(MainImagePath))
                    {
                        FileInfo info = new FileInfo(MainImagePath);

                        MainImagePath = "������ �����\\" + info.Name + "_" + Guid.NewGuid() + "." + info.Extension;
                    }
                    File.WriteAllBytes(MainImagePath, value);
                }
                catch
                {
                    MessageBox.Show("�� ������� ��������� ����������� �� ���� " + MainImagePath + "!", "�� ������� ��������� �����������"
                        , MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateProp();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientService> ClientServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicePhoto> ServicePhotoes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateProp([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
