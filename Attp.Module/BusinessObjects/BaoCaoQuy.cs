﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace Attp.Module.BusinessObjects {
	[NavigationItem(R.V1)]
	[DefaultClassOptions]
	[XafDisplayName("Báo cáo quý")]
	[DefaultProperty(nameof(TenBaoCao))]
	[ImageName("BO_Contact")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	public class BaoCaoQuy : BaseObject {
		public BaoCaoQuy(Session session) : base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
		}

		//protected override void OnLoaded() {
		//	base.OnLoaded();

		//	if (!IsLoading) {
		//		var baocaothang = Session.Query<KyBaoCao>().Where(i => i.BaoCaoQuy.Equals(this)).ToList();
		//		bool pheduyet = true;
		//		foreach (var thang in baocaothang) {
		//			pheduyet = pheduyet && thang.PheDuyet;
		//		}
		//		PheDuyet = pheduyet;
		//	}
		//}

		string tenBaoCao;
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		[XafDisplayName("Tên báo cáo")]
		public string TenBaoCao {
			get => tenBaoCao;
			set => SetPropertyValue(nameof(TenBaoCao), ref tenBaoCao, value);
		}

		int quy;
		[XafDisplayName("Quý")]
		public int Quy {
			get => quy;
			set {
				SetPropertyValue(nameof(Quy), ref quy, value);
				if (!IsLoading)
					TenBaoCao = $"Báo cáo quý {Quy} năm {Nam}";
			}
		}

		int nam;
		[XafDisplayName("Năm"), ModelDefault("DisplayFormat", "{0:G}")]
		public int Nam {
			get => nam;
			set {
				SetPropertyValue(nameof(Nam), ref nam, value);
				if (!IsLoading)
					TenBaoCao = $"Báo cáo quý {Quy} năm {Nam}";
			}
		}

		//bool pheDuyet;
		//[XafDisplayName("Phê duyệt"), ToolTip(""), ModelDefault("AllowEdit", "False")]
		//public bool PheDuyet {
		//	get => pheDuyet;
		//	set => SetPropertyValue(nameof(PheDuyet), ref pheDuyet, value);
		//}

		//[Action(Caption = "Phê duyệt", ConfirmationMessage = "Phê duyệt báo cáo này? Sau khi phê duyệt, dữ liệu của tất cả các kỳ báo cáo tháng thuộc quý này sẽ không thể sửa được nữa.", AutoCommit = true, TargetObjectsCriteria = "[PheDuyet]=False", SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
		//public void PheDuyetAction() {
		//	PheDuyet = true;
		//	var baocaothang = Session.Query<KyBaoCao>().Where(i => i.BaoCaoQuy.Equals(this)).ToList();
		//	foreach (var thang in baocaothang) {
		//		thang.PheDuyet = true;
		//	}
		//}

		#region Associations
		[Association("BaoCaoQuy-KeHoachThamDinhs")]
		[XafDisplayName("Kế hoạch thẩm định")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThamDinh> KeHoachThamDinhs {
			get => GetCollection<KeHoachThamDinh>(nameof(KeHoachThamDinhs));
		}

		[Association("BaoCaoQuy-KeHoachThanhKiemTras")]
		[XafDisplayName("Kế hoạch thanh kiểm tra")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThanhKiemTra> KeHoachThanhKiemTras {
			get => GetCollection<KeHoachThanhKiemTra>(nameof(KeHoachThanhKiemTras));
		}

		[Association("BaoCaoQuy-GiayChungNhans")]
		[XafDisplayName("Giấy chứng nhận đã cấp")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<GiayChungNhan> GiayChungNhans {
			get => GetCollection<GiayChungNhan>(nameof(GiayChungNhans));
		}

		[Association("BaoCaoQuy-DuLieuKiemTraCSSXKDs")]
		[XafDisplayName("Kết quả kiểm tra cơ sở SXKD")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<DuLieuKiemTraCSSXKD> DuLieuKiemTraCSSXKDs => GetCollection<DuLieuKiemTraCSSXKD>(nameof(DuLieuKiemTraCSSXKDs));

		[Association("BaoCaoQuy-VanBanChiDaos")]
		[XafDisplayName("Văn bản chỉ đạo đã ban hành")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<VanBanChiDao> VanBanChiDaos {
			get => GetCollection<VanBanChiDao>(nameof(VanBanChiDaos));
		}

		[Association("BaoCaoQuy-HoatDongSanPhamTruyenThongs")]
		[ModelDefault("AllowEdit", "False")]
		[XafDisplayName("Hoạt động sản phẩm truyền thông đã thực hiện")]
		public XPCollection<HoatDongSanPhamTruyenThong> HoatDongSanPhamTruyenThongs {
			get => GetCollection<HoatDongSanPhamTruyenThong>(nameof(HoatDongSanPhamTruyenThongs));
		}
		#endregion

		private XPCollection<AuditDataItemPersistent> auditTrail;
		[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
		public XPCollection<AuditDataItemPersistent> AuditTrail {
			get {
				if (auditTrail == null) {
					auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
				}
				return auditTrail;
			}
		}
	}
}